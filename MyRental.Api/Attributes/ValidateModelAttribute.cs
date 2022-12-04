using Microsoft.AspNetCore.Mvc.Filters;

namespace MyRental.Api.Attributes;

/// <summary>
/// A filter that provides input model validation
/// </summary>
public class ValidateModelAttribute : ActionFilterAttribute
{
    /// <summary>
    /// Validate userInput model
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            var validationExceptions = context.ModelState
                .Select(m =>
                    new Exception(string.Join(Environment.NewLine, m.Value!.Errors.Select(e => e.ErrorMessage))))
                .Where(x => !string.IsNullOrWhiteSpace(x.Message));
            
            throw new AggregateException(validationExceptions);
        }
    }
}