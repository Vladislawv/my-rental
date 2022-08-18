using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics;
using MyRental.Api.Dto;

namespace MyRental.Api.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlerMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
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

        var result = JsonSerializer.Serialize(errorDto, new JsonSerializerOptions(JsonSerializerDefaults.Web));

        await response.WriteAsync(result);
    }
}