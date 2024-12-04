using Microsoft.AspNetCore.Mvc;
using Restaurants.Domain.Exceptions;
using System.Text.Json;

namespace Restaurants.API.Middlewares
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

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (NotFoundException ex)
            {
                _logger.LogWarning(ex, "A NotFoundException occurred.");
                await HandleExceptionAsync(httpContext, StatusCodes.Status404NotFound, ex.Message);
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning(ex, "A ValidationException occurred.");
                await HandleExceptionAsync(httpContext, StatusCodes.Status400BadRequest, ex.Message);
            }
            catch (UnauthorizedException ex)
            {
                _logger.LogWarning(ex, "An UnauthorizedAccessException occurred.");
                await HandleExceptionAsync(httpContext, StatusCodes.Status401Unauthorized, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred.");
                await HandleExceptionAsync(httpContext, StatusCodes.Status500InternalServerError, "An unexpected error occurred. Please try again later.");
            }
        }

        private async Task HandleExceptionAsync(HttpContext httpContext, int statusCode, string message)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = statusCode;

            var problem = new ProblemDetails
            {
                Status = statusCode,
                Title = GetTitleForStatusCode(statusCode),
                Detail = message,
                Instance = httpContext.Request.Path
            };

            var problemJson = JsonSerializer.Serialize(problem);

            await httpContext.Response.WriteAsync(problemJson);
        }

        private static string GetTitleForStatusCode(int statusCode)
        {
            return statusCode switch
            {
                StatusCodes.Status404NotFound => "Resource Not Found",
                StatusCodes.Status400BadRequest => "Bad Request",
                StatusCodes.Status401Unauthorized => "Unauthorized",
                StatusCodes.Status500InternalServerError => "Internal Server Error",
                _ => "Error"
            };
        }
    }

    public static class ErrorHandlingMiddlewareExtensions
    {
        public static IApplicationBuilder UseErrorHandling(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }
}
