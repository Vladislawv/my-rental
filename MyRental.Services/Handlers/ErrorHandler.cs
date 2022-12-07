using Microsoft.AspNetCore.Identity;

namespace MyRental.Services.Handlers;

public static class ErrorHandler
{
    public static string GetDescriptionByIdentityResult(IdentityResult result)
    {
        return result.Errors.Aggregate("", (current, error) => current + error.Description + " ");
    }
}