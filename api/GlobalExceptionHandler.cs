﻿using System.ComponentModel.DataAnnotations;
using System.Security.Authentication;
using api.DataValidation;

namespace api;

public class GlobalExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;
    private readonly RequestDelegate _next;

    public GlobalExceptionHandler(
        ILogger<GlobalExceptionHandler> logger,
        RequestDelegate next)
    {
        _logger = logger;
        _next = next;
    }

    public async Task InvokeAsync(HttpContext http)
    {
        try
        {
            await _next.Invoke(http);
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(http, exception, _logger);
        }
    }

    private static Task HandleExceptionAsync(HttpContext http, Exception exception,
        ILogger<GlobalExceptionHandler> logger)
    {
        http.Response.ContentType = "application/json";
        logger.LogError(exception, "{ExceptionMessage}", exception.Message);

        if (exception is ValidationException ||
            exception is ArgumentException ||
            exception is ArgumentNullException ||
            exception is ArgumentOutOfRangeException ||
            exception is InvalidCredentialException)
        {
            http.Response.StatusCode = StatusCodes.Status400BadRequest;
        }
        else if (exception is KeyNotFoundException)
        {
            http.Response.StatusCode = StatusCodes.Status404NotFound;
        }
        else if (exception is AuthenticationException)
        {
            http.Response.StatusCode = StatusCodes.Status401Unauthorized;
        }
        else if (exception is UnauthorizedAccessException)
        {
            http.Response.StatusCode = StatusCodes.Status403Forbidden;
        }
        else if (exception is NotSupportedException ||
                 exception is NotImplementedException)
        {
            http.Response.StatusCode = StatusCodes.Status501NotImplemented;
            return http.Response.WriteAsJsonAsync(new ResponseDto { MessageToClient = "Unable to process request" });
        }
        else
        {
            http.Response.StatusCode = StatusCodes.Status500InternalServerError;
            return http.Response.WriteAsJsonAsync(new ResponseDto { MessageToClient = "Unable to process request" });
        }

        return http.Response.WriteAsJsonAsync(new ResponseDto
        {
            MessageToClient = exception.Message
        });
    }
}