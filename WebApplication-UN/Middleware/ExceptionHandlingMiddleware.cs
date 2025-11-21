using System.Net;
using System.Text.Json;
using Library.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using WebApplication_UN.Models;

namespace WebApplication_UN.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(
            RequestDelegate next,
            ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Unhandled exception for request {Method} {Path}",
                    context.Request.Method,
                    context.Request.Path);

                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            response.StatusCode = ex switch
            {
                KeyNotFoundException => (int)HttpStatusCode.NotFound,
                ArgumentException => (int)HttpStatusCode.BadRequest,
                _ => (int)HttpStatusCode.InternalServerError
            };

            var error = new ApiError
            {
                Message = response.StatusCode == (int)HttpStatusCode.InternalServerError
                    ? "An unexpected error occurred."
                    : ex.Message,
         
                Detail = ex is not KeyNotFoundException ? ex.ToString() : null,
                TraceId = context.TraceIdentifier
            };

            var json = JsonSerializer.Serialize(error);
            return response.WriteAsync(json);
        }
    }
}