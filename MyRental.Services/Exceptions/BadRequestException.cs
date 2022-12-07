using System.Net;

namespace MyRental.Services.Exceptions;

public class BadRequestException : CustomException
{
    private const HttpStatusCode StatusCode = HttpStatusCode.BadRequest;
    
    public BadRequestException(string message) : base(message, StatusCode)
    {
    }
}