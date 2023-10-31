using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Repository;
using Repository.Helpers;
using Repository.Models;

namespace Repository.Persistence.Repository
{
    [ExcludeFromCodeCoverage]
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context, DBContext dbContext)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                // Crear la entidad ExceptionLog con los datos de la excepción
                var exceptionLog = new AdminT019ExceptionLog
                {
                    A019Timestamp = DateTime.UtcNow,
                    A019Mensaje = ex.Message,
                    A019Detalles = ex.ToString(),
                    A019Fuente = ex.Source,
                    A019Tipo = ex.GetType().FullName,
                    A019StackTrace = ex.StackTrace,
                    A019Modulo = "CUPOS"

                };
                _logger.LogError(ex, "An error occurred in the method.");
                // Guardar la entidad en la base de datos
                dbContext.AdminT019ExceptionLog.Add(exceptionLog);
                await dbContext.SaveChangesAsync();
            }
        }

        public class MyCustomException : Exception
        {
            public MyCustomException(string message) : base(message)
            {
            }
        }

    }
}