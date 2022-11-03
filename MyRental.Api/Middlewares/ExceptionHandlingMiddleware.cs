using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics;
using MyRental.Api.Dto;

namespace MyRental.Api.Middlewares;

/// <summary>
/// Class to handle exceptions
/// </summary>
public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlerMiddleware> _logger;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="next"></param>
    /// <param name="logger"></param>
    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    /// <summary>
    /// Handle exception and write response
    /// </summary>
    /// <param name="context"></param>
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(ex, context);
        }
    }

    private async Task HandleExceptionAsync(Exception ex, HttpContext context)
    {
        _logger.LogError(ex.Message);
        var response = context.Response;
        
        response.ContentType = "application/json";
        response.StatusCode = (int) HttpStatusCode.InternalServerError;

        ErrorDto errorDto = new()
        {
            ErrorMessage = ex.Message
        };

        await response.WriteAsJsonAsync(errorDto, new JsonSerializerOptions(JsonSerializerDefaults.Web));
    }
}