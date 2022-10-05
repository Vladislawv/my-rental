using System.Text.RegularExpressions;
using FluentValidation;
using FluentValidation.Validators;

namespace MyRental.Services.Validators;

public class PhoneNumberValidator<T> : PropertyValidator<T, string>
{
    public override string Name => "PhoneNumberValidator";
    
    public override bool IsValid(ValidationContext<T> context, string phoneNumber)
    {
        return new Regex("^[1-9]{6,}\\d*$").IsMatch(phoneNumber);
    }

    protected override string GetDefaultMessageTemplate(string errorCode)
    {
        return "Number must have at least 6 digits.";
    }
}