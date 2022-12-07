using FluentValidation;
using MyRental.Services.Areas.Users.Dto;

namespace MyRental.Services.Areas.Users.Validators;

public class LoginDtoValidator : AbstractValidator<LoginDto>
{
    public LoginDtoValidator(IUserService userService)
    {
        RuleFor(login => login.Email)
            .EmailAddress();

        RuleFor(login => login.Password)
            .UserPassword(userService);
    }
}