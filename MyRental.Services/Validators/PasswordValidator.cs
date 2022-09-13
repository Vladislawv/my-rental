using System.Text.RegularExpressions;

namespace MyRental.Services.Validators;

public static class PasswordValidator
{
    //Requirements to password: must contain (upper- and lowerCase char, digit, non alphanumeric char), minimum length: 6. 
    public static bool Validate(string password)
    {
        var expression = "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{6,}$";

        var isPasswordStrong = new Regex(expression).IsMatch(password);
        
        if (!isPasswordStrong) throw new Exception("Password must contain: upperCase, lowerCase, digit, non alphanumeric symbol, minimum length: 6.");

        return true;
    }
}