# Liga Fútbol REST API

API REST para la gestión de ligas de fútbol desarrollada con .NET Core 9, C#, SQL Server y Entity Framework.

## 📋 Características

- **Autenticación JWT Bearer**: Sistema de autenticación seguro
- **Database First**: Enfoque Entity Framework Database First
- **Arquitectura en Capas**: Separación de responsabilidades (Controllers, Services, Repositories)
- **Patrones de Diseño**: Repository Pattern, Service Layer, Dependency Injection
- **Principios SOLID**: Código mantenible y escalable
- **Documentación Swagger**: Interfaz de pruebas integrada
- **Respuestas JSON**: Formato estándar de respuestas

## 🏗️ Arquitectura

```
LigaFutbolApi/
├── Controllers/         # Controladores API
├── Services/           # Lógica de negocio
│   └── Interfaces/
├── Repositories/       # Acceso a datos
│   └── Interfaces/
├── Models/            # Entidades del dominio
├── DTOs/              # Data Transfer Objects
└── Data/              # DbContext
```

## 🚀 Requisitos Previos

- .NET Core 9 SDK
- SQL Server 2019 o superior
- Visual Studio 2022 / VS Code / Rider

## 📦 Instalación

### 1. Clonar o crear el proyecto

```bash
dotnet new webapi -n LigaFutbolApi
cd LigaFutbolApi
```

### 2. Instalar paquetes NuGet

```bash
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Tools
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
dotnet add package Swashbuckle.AspNetCore
```

### 3. Configurar la base de datos

**Opción A: Ejecutar el script SQL**

1. Abrir SQL Server Management Studio
2. Conectar a tu instancia de SQL Server
3. Ejecutar el archivo `CreateDatabase.sql`

**Opción B: Crear manualmente**

```sql
CREATE DATABASE LigaFutbolDB;
-- Luego ejecutar el resto del script
```

### 4. Configurar cadena de conexión

Editar `appsettings.json` con tu cadena de conexión:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=TU_SERVIDOR;Database=LigaFutbolDB;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

Para SQL Server con usuario y contraseña:
```json
"DefaultConnection": "Server=TU_SERVIDOR;Database=LigaFutbolDB;User Id=TU_USUARIO;Password=TU_PASSWORD;TrustServerCertificate=True;"
```

### 5. Aplicar migraciones (Database First)

Para generar el modelo desde la base de datos existente:

```bash
dotnet ef dbcontext scaffold "Server=localhost;Database=LigaFutbolDB;Trusted_Connection=True;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -o Models -c LigaFutbolContext --context-dir Data --force
```

### 6. Ejecutar la aplicación

```bash
dotnet run
```

La API estará disponible en:
- HTTP: `http://localhost:5000`
- HTTPS: `https://localhost:5001`
- Swagger: `https://localhost:5001` (redirige a Swagger UI)

## 🔐 Autenticación

### Usuario por defecto

El script de base de datos crea un usuario administrador:

- **Email**: admin@ligafutbol.com
- **Password**: Admin123

### Obtener token JWT

**Endpoint**: `POST /api/auth/login`

```json
{
  "email": "admin@ligafutbol.com",
  "password": "Admin123"
}
```

**Respuesta**:
```json
{
  "success": true,
  "message": "Inicio de sesión exitoso",
  "data": {
    "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
    "email": "admin@ligafutbol.com",
    "rol": "Administrador",
    "expiration": "2025-10-07T21:00:00Z"
  },
  "errors": []
}
```

### Usar el token

En Swagger, hacer clic en el botón "Authorize" y pegar:
```
Bearer TU_TOKEN_AQUI
```

En Postman/otras herramientas, agregar header:
```
Authorization: Bearer TU_TOKEN_AQUI
```

## 📚 Endpoints Principales

### Autenticación
- `POST /api/auth/login` - Iniciar sesión
- `POST /api/auth/register` - Registrar usuario

### Ligas
- `GET /api/ligas` - Obtener todas las ligas
- `GET /api/ligas/{id}` - Obtener liga por ID
- `POST /api/ligas` - Crear liga (Admin)
- `PUT /api/ligas/{id}` - Actualizar liga (Admin)
- `DELETE /api/ligas/{id}` - Eliminar liga (Admin)

### Torneos
- `GET /api/torneos` - Obtener todos los torneos
- `GET /api/torneos/{id}` - Obtener torneo por ID
- `GET /api/torneos/liga/{ligaId}` - Obtener torneos de una liga
- `POST /api/torneos` - Crear torneo
- `PUT /api/torneos/{id}` - Actualizar torneo
- `DELETE /api/torneos/{id}` - Eliminar torneo

### Equipos
- `GET /api/equipos` - Obtener todos los equipos
- `GET /api/equipos/{id}` - Obtener equipo por ID
- `GET /api/equipos/torneo/{torneoId}` - Obtener equipos de un torneo
- `POST /api/equipos` - Crear equipo
- `PUT /api/equipos/{id}` - Actualizar equipo
- `DELETE /api/equipos/{id}` - Eliminar equipo

### Jugadores
- `GET /api/jugadores` - Obtener todos los jugadores
- `GET /api/jugadores/{id}` - Obtener jugador por ID
- `GET /api/jugadores/equipo/{equipoId}` - Obtener jugadores de un equipo
- `POST /api/jugadores` - Crear jugador
- `PUT /api/jugadores/{id}` - Actualizar jugador
- `DELETE /api/jugadores/{id}` - Eliminar jugador

## 🔨 Estructura del Proyecto

### Models (Entidades)
- Usuario
- Rol
- Liga
- Torneo
- Equipo
- Jugador
- Partido
- Estadistica
- TipoEstadistica
- Castigo
- TipoCastigo
- ProgramacionJuego

### Patrones Implementados

**Repository Pattern**: Abstracción del acceso a datos
```csharp
public interface IRepository<T> where T : class
{
    Task<T> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> AddAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task<bool> DeleteAsync(int id);
}
```

**Service Layer**: Lógica de negocio separada
```csharp
public interface ILigaService
{
    Task<ApiResponse<LigaDto>> GetByIdAsync(int id);
    Task<ApiResponse<LigaDto>> CreateAsync(CreateLigaDto dto);
    // ...
}
```

**Dependency Injection**: Inversión de control
```csharp
builder.Services.AddScoped<ILigaRepository, LigaRepository>();
builder.Services.AddScoped<ILigaService, LigaService>();
```

## 📝 Formato de Respuestas

Todas las respuestas siguen el formato estándar:

```json
{
  "success": true,
  "message": "Operación exitosa",
  "data": { },
  "errors": []
}
```

### Respuesta exitosa
```json
{
  "success": true,
  "message": "Liga obtenida exitosamente",
  "data": {
    "id": 1,
    "nombre": "Liga MX",
    "logo": "https://...",
    "estado": true,
    "creado": "2025-10-07T00:00:00Z",
    "modificado": "2025-10-07T00:00:00Z"
  },
  "errors": []
}
```

### Respuesta con error
```json
{
  "success": false,
  "message": "Error al obtener la liga",
  "data": null,
  "errors": [
    "No existe una liga con el ID 999"
  ]
}
```

## 🔒 Roles y Permisos

- **Admin**: Acceso completo a todos los endpoints
- **DT (Director Técnico)**: Gestión de equipos y jugadores
- **Arbitro**: Acceso a partidos y estadísticas
- **Jugador**: Acceso de solo lectura

## 🧪 Pruebas con Swagger

1. Ejecutar la aplicación: `dotnet run`
2. Navegar a: `https://localhost:5001`
3. Hacer clic en "Authorize"
4. Obtener token desde `/api/auth/login`
5. Pegar el token en formato: `Bearer TOKEN`
6. Probar los endpoints

## 📊 Diagrama de Base de Datos

```
Liga (1) ──< Torneo (*)
Torneo (1) ──< Equipo (*)
Equipo (1) ──< Jugador (*)
Equipo (2) ──< Partido (*)
Partido (1) ──< Estadistica (*)
Jugador (1) ──< Estadistica (*)
Jugador (1) ──< Castigo (*)
```

## 🛠️ Próximos Pasos

Ahora que tienes la REST API funcionando, los siguientes pasos serían:

1. **Implementar controladores restantes**: Torneos, Equipos, Jugadores, Partidos, etc.
2. **Agregar validaciones de negocio**: Reglas específicas del dominio
3. **Implementar logging**: Registrar eventos y errores
4. **Agregar paginación**: Para endpoints que retornan listas grandes
5. **Implementar caché**: Mejorar performance con Redis
6. **Tests unitarios**: Cobertura de código con xUnit
7. **CI/CD**: Pipeline de integración continua

## 📞 Soporte

Para preguntas o problemas:
- Revisar la documentación de Swagger
- Verificar logs de la aplicación
- Validar cadena de conexión a la base de datos

## 📄 Licencia

Este proyecto es de uso educativo y puede ser modificado según necesidades específicas.