using System.Text.RegularExpressions;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyRental.Infrastructure.Entities;
using MyRental.Services.Areas.Users.Dto;

namespace MyRental.Services.Validators;

public class UserDtoInputValidator : AbstractValidator<UserDtoInput>
{
    public UserDtoInputValidator(UserManager<User> userManager)
    {
        RuleFor(user => user.UserName).MinimumLength(4).MaximumLength(10).Custom((name, context) =>
        {
            var isNameUsed = userManager.Users.AnyAsync(user => user.UserName == name).Result;
                
            if (isNameUsed) context.AddFailure("This name has already used.");
        });
        
        //@d == true
        RuleFor(user => user.Email).EmailAddress().WithMessage("Email has already used or wrong.");
        
        RuleFor(user => user.PhoneNumber).Custom((number, context) =>
        {
            var regEx = new Regex("^[1-9]{8}\\d*$");

            var isNumberValid = regEx.IsMatch(number);
            if (!isNumberValid) context.AddFailure("Number is wrong.");
        });

        RuleFor(user => user.Password).Custom((password, context) =>
        {
            var regEx = new Regex("^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])|(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[^a-zA-Z0-9])|(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])|(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])).{8,}$");

            var isPasswordValid = regEx.IsMatch(password);
            if (!isPasswordValid) context.AddFailure("Password must contain: at least one digit, upper- and lowerCase character, non alpha-numeric character. Minimum length: 8");
        });
    }
}