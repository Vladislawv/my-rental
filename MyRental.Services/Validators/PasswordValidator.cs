using System.Text.RegularExpressions;

namespace MyRental.Services.Validators;

public static class PasswordValidator
{
    //Requirements to password: must contain (upper- and lowerCase char, digit, non alphanumeric char), minimum length: 6. 
    public static bool Validate(string password)
    {
        var expression = "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{6,}$";

        return new Regex(expression).IsMatch(password);
    }
}