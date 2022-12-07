using FluentValidation;

namespace MyRental.Services.Areas.Notifications.Validators;

public class EmailValidator : AbstractValidator<string>
{
    public EmailValidator()
    {
        RuleFor(email => email)
            .EmailAddress();
    }
}