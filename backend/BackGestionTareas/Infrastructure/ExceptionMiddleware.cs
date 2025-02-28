using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BackGestionTareas.Infrastructure
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException ex)
            {
                var errors = ex.Errors.Select(error => new
                {
                    propertyName = error.PropertyName,
                    errorMessage = error.ErrorMessage
                });

                var response = new
                {
                    message = "Error de validación",
                    errors
                };

                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                await context.Response.WriteAsJsonAsync(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocurrió un error inesperado");

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var response = _env.IsDevelopment()
                ? new { message = ex.Message, stackTrace = ex.StackTrace ?? "" }
                : new { message = "Ocurrió un error inesperado. Inténtelo más tarde.", stackTrace = "" };

                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}