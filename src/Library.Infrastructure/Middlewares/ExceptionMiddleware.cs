using Library.Application.ApiModels;
using Library.Application.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Globalization;

namespace Library.Infrastructure.Middlewares
{
    internal sealed class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (BadRequestException ex)
            {
                await WrapExceptionAsync(context,400,ex.Errors);
            }
            catch (NotFoundException ex)
            {
                await WrapExceptionAsync(context, 404, ex.Errors);
            }
        }

        private async Task WrapExceptionAsync(HttpContext context, int statusCode, string[] errors)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = statusCode;
            await response.WriteAsync(JsonConvert.SerializeObject(ApiResponse.Failure(statusCode, errors)));
        }
    }

    public static class RequestCultureMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionMiddleware(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
