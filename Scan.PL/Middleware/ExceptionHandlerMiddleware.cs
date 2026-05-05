using Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Shared.ErrorModels;

using System.Text.Json;

namespace Scan.PL.Middleware
{
    /// <summary>
    /// Returns a single user-friendly error string to the client. Never exposes internal details.
    /// </summary>
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;

        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);

                if (context.Response.StatusCode == StatusCodes.Status404NotFound)
                {
                    var response = new ErrorToReturn
                    {
                        StatusCode = context.Response.StatusCode,
                        ErrorMessage = $" End Point {context.Request.Path} is not found"
                    };
                    context.Response.ContentType = "application/json";

                    var ResponseToReturn = JsonSerializer.Serialize(response);

                    await context.Response.WriteAsync(ResponseToReturn);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "something wrong");

                var response = new ErrorToReturn
                {

                    ErrorMessage = ex switch
                    {
                        BadRequestException => ex.Message,
                        NotFoundException => ex.Message,
                        UnauthorizedException => ex.Message,


                        _ => ex.Message
                    }

                };
                response.StatusCode = ex switch
                {
                    NotFoundException => StatusCodes.Status404NotFound,
                    UnauthorizedException => StatusCodes.Status401Unauthorized,
                    BadRequestException badRequestException
                    => StatusCodes.Status500InternalServerError,
                    _ =>
                     StatusCodes.Status500InternalServerError
                };
                context.Response.StatusCode = response.StatusCode;
                context.Response.ContentType = "application/json";


                var ResponseToReturn = JsonSerializer.Serialize(response);
                


                await context.Response.WriteAsync(ResponseToReturn);
            }
        }
        //private static int GetBadRequestErrors(BadRequestException badRequestException, ErrorToReturn response)
        //{
        //    response.Errors = badRequestException.Errors;
        //    return StatusCodes.Status400BadRequest;
        //}

    }
}
