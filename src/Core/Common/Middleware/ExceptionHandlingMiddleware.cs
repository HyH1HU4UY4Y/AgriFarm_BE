﻿using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SharedDomain.Common;
using SharedDomain.Exceptions;
using System.Linq;
using System.Text.Json;

namespace SharedApplication.Middleware
{
    public class ExceptionHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger) => _logger = logger;
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                await HandleExceptionAsync(context, e);
            }
        }
        private static async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            var statusCode = GetStatusCode(exception);

            var response = new DefaultResponse<List<string>>
            {
                Data = new()
                {
                    $"{GetTitle(exception)}",
                },
                Message = exception.Message ,
                Status = statusCode
            };
            if (GetErrors(exception).Any())
            {
                response.Data.AddRange(GetErrors(exception));
            }
                
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = statusCode;
            await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(response));
        }
        private static int GetStatusCode(Exception exception) =>
            exception switch
            {
                BadRequestException => StatusCodes.Status400BadRequest,
                NotFoundException => StatusCodes.Status404NotFound,
                ValidationException => StatusCodes.Status422UnprocessableEntity,
                _ => StatusCodes.Status500InternalServerError
            };
        private static string GetTitle(Exception exception) =>
            exception switch
            {
                ApplicationException applicationException => applicationException.InnerException!.Message,
                _ => "Server Error"
            };
        private static List<string> GetErrors(Exception exception)
        {
            List<string> errors = new();
            if (exception is ValidationException validationException)
            {
                errors = validationException.Errors
                    //.ToDictionary(x=>x.Key, x => x.Value)
                    .Select(x => $"{x.Key}: {string.Join(", ", x.Value)}")
                    .ToList();
            }
            return errors;
        }
    }
}
