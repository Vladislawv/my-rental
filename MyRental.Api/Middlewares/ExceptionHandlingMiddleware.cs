using System.Net;
using System.Net.Mime;
using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics;
using MyRental.Api.Dto;
using MyRental.Services.Exceptions;

namespace MyRental.Api.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlerMiddleware> _logger;
    private const string InternalServerErrorMessage = "Internal Server Error";
    
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
            await HandleExceptionAsync(e, context);
        }
    }

    private async Task HandleExceptionAsync(Exception ex, HttpContext context)
    {
        _logger.LogError(ex.Message);
        
        var response = context.Response;
        response.ContentType = MediaTypeNames.Application.Json;
        
        var errorDto = new ErrorDto();

        if (ex is CustomException customException)
        {
            response.StatusCode = (int)customException.ResponseStatusCode;
            errorDto.ErrorMessage = customException.Message;
            errorDto.AdditionalInfo = customException.ResponseAdditionalInfo;
        }
        else
        {
            response.StatusCode = (int)HttpStatusCode.InternalServerError;
            errorDto.ErrorMessage = InternalServerErrorMessage;
        }
        
        await response.WriteAsJsonAsync(errorDto, new JsonSerializerOptions(JsonSerializerDefaults.Web));
    }
}