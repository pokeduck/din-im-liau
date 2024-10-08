
using System.ComponentModel.DataAnnotations;
using Common.Helper;

namespace Models.Attributes;

public class EmailRequiredAttribute : RequiredAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var valString = value?.ToString();

        if (string.IsNullOrEmpty(valString))
        {
            return new ValidationResult("Email Empty!");
        }
        else if (!RegexHelper.IsValidEmail(valString))
        {
            return new ValidationResult("Email wrong format.");
        }

        return ValidationResult.Success;
    }
}
