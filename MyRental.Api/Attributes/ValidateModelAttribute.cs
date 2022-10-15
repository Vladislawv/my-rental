using Microsoft.AspNetCore.Mvc.Filters;
using MyRental.Services.Areas.Ads.Dto;
using MyRental.Services.Areas.Ads.Validators;
using MyRental.Services.Areas.Users.Dto;
using MyRental.Services.Areas.Users.Services;
using MyRental.Services.Areas.Users.Validators;

namespace MyRental.Api.Attributes;

/// <summary>
/// A filter that provides input model validation
/// </summary>
public class ValidateModelAttribute : ActionFilterAttribute
{
    private readonly IUserService _userService;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="userService"></param>
    public ValidateModelAttribute(IUserService userService)
    {
        _userService = userService;
    }

    /// <summary>
    /// Validate userInput model
    /// </summary>
    /// <param name="context"></param>
    /// <param name="next"></param>
    /// <returns></returns>
    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (context.ActionArguments.ContainsKey("userInput")) {
            var userInput = (UserDtoInput)(context.ActionArguments["userInput"] 
                ?? throw new Exception("The input is empty!"));

            await new UserDtoInputValidator(_userService).ValidateAsync(userInput);
        }

        if (context.ActionArguments.ContainsKey("adInput"))
        {
            var adInput = (AdDtoInput)(context.ActionArguments["adInput"]
                ?? throw new Exception("The input is empty!"));

            await new AdDtoInputValidator().ValidateAsync(adInput);
        }

        await next.Invoke();
    }
}