using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SLMS.Infrastructure.Middleware
{
    /// <summary>
    /// Middleware para logging de requests
    /// Clean Code: Separación de responsabilidades
    /// Mejores Prácticas: Logging estructurado, performance monitoring
    /// </summary>
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var stopwatch = Stopwatch.StartNew();
            var requestId = Guid.NewGuid().ToString();

            // Agregar requestId al contexto
            context.Items["RequestId"] = requestId;

            try
            {
                _logger.LogInformation(
                    "Request iniciado: {RequestId} {Method} {Path}",
                    requestId,
                    context.Request.Method,
                    context.Request.Path);

                await _next(context);

                stopwatch.Stop();

                _logger.LogInformation(
                    "Request completado: {RequestId} {Method} {Path} {StatusCode} - {ElapsedMilliseconds}ms",
                    requestId,
                    context.Request.Method,
                    context.Request.Path,
                    context.Response.StatusCode,
                    stopwatch.ElapsedMilliseconds);

                // Alertar si el request es lento (> 3 segundos)
                if (stopwatch.ElapsedMilliseconds > 3000)
                {
                    _logger.LogWarning(
                        "Request LENTO detectado: {RequestId} {Method} {Path} - {ElapsedMilliseconds}ms",
                        requestId,
                        context.Request.Method,
                        context.Request.Path,
                        stopwatch.ElapsedMilliseconds);
                }
            }
            catch (Exception ex)
            {
                stopwatch.Stop();

                _logger.LogError(ex,
                    "Request falló: {RequestId} {Method} {Path} - {ElapsedMilliseconds}ms",
                    requestId,
                    context.Request.Method,
                    context.Request.Path,
                    stopwatch.ElapsedMilliseconds);

                throw;
            }
        }
    }
}
