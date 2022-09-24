using FluentValidation;
using Microsoft.AspNetCore.Mvc.Filters;
using MyRental.Services.Areas.Users.Dto;

namespace MyRental.Api.Attributes;

/// <summary>
/// A filter that provides input model validation
/// </summary>
public class ValidateModelAttribute : ActionFilterAttribute
{
    private readonly IValidator<UserDtoInput> _validator;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="validator"></param>
    public ValidateModelAttribute(IValidator<UserDtoInput> validator)
    {
        _validator = validator;
    }

    /// <summary>
    /// Validate input model
    /// </summary>
    /// <param name="context"></param>
    /// <param name="next"></param>
    /// <returns></returns>
    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var value = context.ActionArguments["userInput"]
                    ?? throw new Exception("The input is empty!");

        var userInput = (UserDtoInput)value;

        var result = await _validator.ValidateAsync(userInput);
        if (!result.IsValid) throw new Exception(result.Errors.Aggregate("", (current, error) => current + error.ErrorMessage + " "));
    }
}