using Microsoft.AspNetCore.Identity;

namespace MyRental.Services.Handlers;

public static class ErrorHandler
{
    public static void GetDescriptionFromErrors(IdentityResult result)
    {
        throw new Exception(result.Errors.Aggregate("", (current, error) => current + error.Description + " "));
    }
}