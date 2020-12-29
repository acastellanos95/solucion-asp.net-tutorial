using System;
using System.Net;
using System.Threading.Tasks;
using Aplicacion.ErrorHandling;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace WebAPI.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next; 
        private readonly ILogger<ErrorHandlingMiddleware> _logger;
        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await AsynchronousErrorHandling(context, ex, _logger);
            }
        }

        private async Task AsynchronousErrorHandling(HttpContext context, Exception ex, ILogger<ErrorHandlingMiddleware> logger)
        {
            object errores = null; 
            switch (ex)
            {
                case ExceptionHandling eh:
                    logger.LogError(ex, "Error Handling");
                    errores =  eh.Errores;
                    context.Response.StatusCode = (int) eh.Codigo;
                    break;
                case Exception e:
                    logger.LogError(e, "Server Error");
                    errores = string.IsNullOrWhiteSpace(e.Message) ? "Error" : e.Message;
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
                default:
                    break;
            }
            context.Response.ContentType = "application/json";
            if (errores != null)
            {
                var resultados = JsonConvert.SerializeObject(new {errores});
                await context.Response.WriteAsync(resultados);
            }
        }
    }
}