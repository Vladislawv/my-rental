using Microsoft.AspNetCore.Mvc.Filters;
using MyRental.Services.Exceptions;

namespace MyRental.Api.Attributes;

public class ValidateModelAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            var fieldErrors = context.ModelState
                .Select(m => new KeyValuePair<string, string>(
                    m.Key, string.Join(Environment.NewLine, m.Value!.Errors.Select(e => e.ErrorMessage))))
                .Where(x => !string.IsNullOrWhiteSpace(x.Value))
                .ToDictionary(x => x.Key, x => x.Value);
            
            throw new ValidationErrorException(fieldErrors);
        }
    }
}