
using System.ComponentModel.DataAnnotations;
using Common.Helper;

namespace Models.Attributes;

public class PasswordRequiredAttribute : RequiredAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var valString = value?.ToString();

        if (string.IsNullOrEmpty(valString))
        {
            return new ValidationResult("Password Empty!");
        }
        else if (!RegexHelper.IsValidPassword(valString))
        {
            return new ValidationResult("Password wrong format.");
        }

        return ValidationResult.Success;
    }
}
