using FluentValidation;
using MyRental.Services.Areas.Users.Dto;

namespace MyRental.Services.Validators;

public class UserDtoInputValidator : AbstractValidator<UserDtoInput>
{
    public UserDtoInputValidator(IUserService userService)
    {
        RuleFor(user => user.Email)
            .EmailAddress();

        RuleFor(user => user.PhoneNumber)
            .PhoneNumber();

        RuleFor(user => user.Password)
            .UserPassword(userService);
    }
}