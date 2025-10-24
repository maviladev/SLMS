using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using LigaFutbolApi.Data;
using LigaFutbolApi.Infrastructure.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// ==================== LOGGING CON SERILOG ====================
// Mejora: Logging estructurado profesional
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File("logs/ligafutbol-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

// ==================== DATABASE ====================
builder.Services.AddDbContext<LigaFutbolContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlOptions =>
        {
            sqlOptions.EnableRetryOnFailure(
                maxRetryCount: 3,
                maxRetryDelay: TimeSpan.FromSeconds(5),
                errorNumbersToAdd: null);
        });

    // Solo en desarrollo
    if (builder.Environment.IsDevelopment())
    {
        options.EnableSensitiveDataLogging();
        options.EnableDetailedErrors();
    }
});

// ==================== AUTHENTICATION ====================
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var googleSettings = builder.Configuration.GetSection("GoogleAuth");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ClockSkew = TimeSpan.Zero, // Sin margen de expiración
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]))
    };

    // Eventos para logging
    options.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            Log.Warning($"Autenticación fallida: {context.Exception.Message}");
            return Task.CompletedTask;
        },
        OnTokenValidated = context =>
        {
            var email = context.Principal?.FindFirst("email")?.Value;
            Log.Information($"Token validado para: {email}");
            return Task.CompletedTask;
        }
    };
});

builder.Services.AddAuthorization(options =>
{
    // Políticas personalizadas
    options.AddPolicy("AdminOnly", policy =>
        policy.RequireRole("Administrador"));

    options.AddPolicy("AdminOrOperator", policy =>
        policy.RequireRole("Administrador", "Operador"));
});

// ==================== DEPENDENCY INJECTION ====================
// SOLID: Dependency Inversion - depender de abstracciones

// Extension Method para mantener Program.cs limpio
builder.Services.AddRepositories();
builder.Services.AddApplicationServices();
builder.Services.AddCommandHandlers();
builder.Services.AddQueryHandlers();

// ==================== HTTP CLIENT PARA GOOGLE ====================
builder.Services.AddHttpClient();

// ==================== CORS ====================
builder.Services.AddCors(options =>
{
    options.AddPolicy("ProductionPolicy", policy =>
    {
        var allowedOrigins = builder.Configuration
            .GetSection("AllowedOrigins")
            .Get<string[]>() ?? Array.Empty<string>();

        policy.WithOrigins(allowedOrigins)
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
    });

    // Solo para desarrollo
    if (builder.Environment.IsDevelopment())
    {
        options.AddPolicy("DevelopmentPolicy", policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
    }
});

// ==================== CONTROLLERS ====================
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // Configuración JSON
        options.JsonSerializerOptions.PropertyNamingPolicy = null; // PascalCase
        options.JsonSerializerOptions.DefaultIgnoreCondition =
            System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
    });

// ==================== API VERSIONING ====================
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
});

// ==================== SWAGGER ====================
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Liga Fútbol API",
        Version = "v1",
        Description = "API REST para gestión de ligas de fútbol con arquitectura CQRS",
        Contact = new OpenApiContact
        {
            Name = "Liga Fútbol",
            Email = "contacto@ligafutbol.com"
        },
        License = new OpenApiLicense
        {
            Name = "MIT License"
        }
    });

    // Configuración de seguridad Bearer
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header usando el esquema Bearer. " +
                      "Ingrese 'Bearer' [espacio] y luego su token. " +
                      "Ejemplo: 'Bearer eyJhbGciOiJIUzI1Ni...'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });

    // Incluir comentarios XML
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath);
    }
});

// ==================== HEALTH CHECKS ====================
builder.Services.AddHealthChecks()
    .AddSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        name: "database",
        timeout: TimeSpan.FromSeconds(3));

// ==================== RESPONSE CACHING ====================
builder.Services.AddResponseCaching();
builder.Services.AddMemoryCache();

// ==================== BUILD APP ====================
var app = builder.Build();

// ==================== MIDDLEWARE PIPELINE ====================
// El orden es CRÍTICO

// 1. Exception Handler (primero para capturar todo)
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/error");
    app.UseHsts();
}

// 2. Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Liga Fútbol API V1");
        c.RoutePrefix = string.Empty;
        c.DocumentTitle = "Liga Fútbol API - Documentación";
        c.DisplayRequestDuration();
    });
}

// 3. HTTPS Redirection
app.UseHttpsRedirection();

// 4. Routing
app.UseRouting();

// 5. CORS (antes de Authentication)
app.UseCors(app.Environment.IsDevelopment() ? "DevelopmentPolicy" : "ProductionPolicy");

// 6. Response Caching
app.UseResponseCaching();

// 7. Authentication (antes de Authorization)
app.UseAuthentication();

// 8. Authorization
app.UseAuthorization();

// 9. Custom Middleware
app.UseMiddleware<RequestLoggingMiddleware>();

// 10. Endpoints
app.MapControllers();
app.MapHealthChecks("/health");

// ==================== SEED DATA ====================
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<LigaFutbolContext>();

        // Aplicar migraciones pendientes
        if (app.Environment.IsDevelopment())
        {
            await context.Database.MigrateAsync();
        }

        // Seed data inicial
        await DbInitializer.SeedAsync(context);

        Log.Information("Base de datos inicializada correctamente");
    }
    catch (Exception ex)
    {
        Log.Fatal(ex, "Error al inicializar la base de datos");
        throw;
    }
}

// ==================== RUN ====================
try
{
    Log.Information("Iniciando Liga Fútbol API");
    await app.RunAsync();
}
catch (Exception ex)
{
    Log.Fatal(ex, "La aplicación falló al iniciar");
}
finally
{
    Log.CloseAndFlush();
}