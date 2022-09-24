using System.Text.RegularExpressions;
using FluentValidation;
using FluentValidation.Validators;

namespace MyRental.Services.Validators;

public class PhoneValidator<T> : PropertyValidator<T, string>
{
    public override bool IsValid(ValidationContext<T> context, string numberPhone)
    {
        var isNumberValid = new Regex("^[1-9]{8}\\d*$").IsMatch(numberPhone);
        if (!isNumberValid) context.AddFailure("Number is wrong!");

        return isNumberValid;
    }

    public override string Name => "PhoneValidator";
}