using System.Net;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;
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
        catch (Exception e)
        {
            await HandleExceptionAsync(context, e.Message, HttpStatusCode.InternalServerError);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, string exMsg, HttpStatusCode httpStatusCode)
    {
        _logger.LogError(exMsg);

        HttpResponse response = context.Response;
        
        response.ContentType = "application/json";
        response.StatusCode = (int) httpStatusCode;

        ErrorDto errorDto = new()
        {
            ErrorMessage = exMsg
        };

        string result = JsonSerializer.Serialize(errorDto, new JsonSerializerOptions(JsonSerializerDefaults.Web));

        await response.WriteAsync(result);
    }
}