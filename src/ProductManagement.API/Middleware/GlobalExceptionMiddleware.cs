using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ProductManagement.API.Models;
using System;
using System.Net;
using System.Threading.Tasks;

namespace ProductManagement.API.Middleware {
    public class GlobalExceptionMiddleware {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger) {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext) {
            try {
                await _next(httpContext);
            }
            catch (Exception ex) {
                _logger.LogError(ex, "An unexpected error occurred.");
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception) {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var errorResponse = new ErrorResponse(context.Response.StatusCode, "An unexpected error occurred.", exception.Message);

            // Log the exception or handle different exception types here if necessary

            return context.Response.WriteAsJsonAsync(errorResponse);
        }
    }
}
