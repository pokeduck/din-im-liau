
using System.ComponentModel.DataAnnotations;
using Common.Helper;

namespace Models.Attributes;

public class EmailOptionalAttribute : RequiredAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var valString = value?.ToString();

        if (string.IsNullOrEmpty(valString))
        {
            if (valString != null)
            {
                return new ValidationResult("Email can't contain only spaces.");
            }
        }
        else if (!RegexHelper.IsValidEmail(valString))
        {
            return new ValidationResult("Email wrong format.");
        }

        return ValidationResult.Success;
    }
}
