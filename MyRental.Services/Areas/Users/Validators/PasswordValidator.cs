using FluentValidation;
using FluentValidation.Validators;
using MyRental.Services.Areas.Users.Services;

namespace MyRental.Services.Areas.Users.Validators;

public class PasswordValidator<T> : PropertyValidator<T, string>
{
    private readonly IUserService _userService;
    private string _errorMessage;

    public PasswordValidator(IUserService userService)
    {
        _userService = userService;
    }

    public override string Name => "PasswordValidator";
    
    public override bool IsValid(ValidationContext<T> context, string password)
    {
        var (result, errorMessage) = _userService.ValidatePasswordAsync(password).GetAwaiter().GetResult();
        if (!result) _errorMessage = errorMessage;

        return result;
    }
    
    protected override string GetDefaultMessageTemplate(string errorCode)
    {
        return _errorMessage;
    }
}