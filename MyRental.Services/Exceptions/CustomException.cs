using System.Net;

namespace MyRental.Services.Exceptions;

public abstract class CustomException : Exception
{
    public HttpStatusCode ResponseStatusCode { get; }
    public object? ResponseAdditionalInfo { get; }
    
    protected CustomException(string message, HttpStatusCode responseStatusCode, object? responseAdditionalInfo = null) 
        : base(message)
    {
        ResponseStatusCode = responseStatusCode;
        ResponseAdditionalInfo = responseAdditionalInfo;
    }
}