using System.Net;

namespace MyRental.Services.Exceptions;

public class NotFoundException : CustomException
{
    private const HttpStatusCode StatusCode = HttpStatusCode.NotFound;

    public NotFoundException(string message) : base(message, StatusCode)
    {
    }
}