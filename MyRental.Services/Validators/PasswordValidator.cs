using FluentValidation;
using FluentValidation.Validators;

namespace MyRental.Services.Validators;

public class PasswordValidator<T> : PropertyValidator<T, string>
{
    private readonly IUserService _userService;

    public PasswordValidator(IUserService userService)
    {
        _userService = userService;
    }

    public override bool IsValid(ValidationContext<T> context, string password)
    {
        var (result, errorMessage) = _userService.ValidatePasswordAsync(password).GetAwaiter().GetResult();
        if (!result) context.AddFailure(errorMessage);

        return result;
    }

    public override string Name => "PasswordValidator";
}