using System.Net;

namespace MyRental.Services.Exceptions;

public class ValidationErrorException : CustomException
{
    private const string ValidationErrorMessage = "Input data has not passed the validation";
    private const HttpStatusCode StatusCode = HttpStatusCode.BadRequest;
    
    public ValidationErrorException(IDictionary<string, string> fieldErrors)
        : base(ValidationErrorMessage, StatusCode, fieldErrors)
    {
    }
}