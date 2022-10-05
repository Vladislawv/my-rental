using FluentValidation;
using MyRental.Services.UserService;

namespace MyRental.Services.Validators;

public static class ValidationRuleBuilderExtensions
{
    public static IRuleBuilderOptions<T, string> PhoneNumber<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder.SetValidator(new PhoneNumberValidator<T>());
    }
    
    public static IRuleBuilderOptions<T, string> UserPassword<T>(this IRuleBuilder<T, string> ruleBuilder, IUserService userService)
    {
        return ruleBuilder.SetValidator(new PasswordValidator<T>(userService));
    }
}