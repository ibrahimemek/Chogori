using Catalog.Domain.Exceptions;
using System.Net;
using System.Text.Json;

namespace Catalog.API.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)   
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            } catch(Exception e)
            {
                _logger.LogError(e, "Error occurred: {Message}", e.Message);
                await HandleExceptionAsync(context, e);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            HttpStatusCode code = HttpStatusCode.InternalServerError;
            string result = "A system error occurred.";
            if (exception is DomainException)
            {
                code = HttpStatusCode.BadRequest;
                result = exception.Message;
            }
            else if (exception is NotFoundException)
            {
                code = HttpStatusCode.NotFound;
                result = exception.Message;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            var responsePayload = JsonSerializer.Serialize(new { error = result });
            return context.Response.WriteAsync(responsePayload);
        }
    }
}
