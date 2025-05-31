using System;
using System.ComponentModel.DataAnnotations;

namespace Incidents.Service.Core.Attributes;

/// <summary>
///   <para>Attribute to validate if a Guid field is not null or empty.</para>
/// </summary>
public class NotEmptyGuid : ValidationAttribute
{
    /// <summary>Returns true if Guid is valid and not empty.</summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="validationContext">The context information about the validation operation.</param>
    /// <returns>An instance of the <see cref="T:System.ComponentModel.DataAnnotations.ValidationResult">ValidationResult</see> class.</returns>
    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        return value is Guid guidValue && guidValue != Guid.Empty
            ? ValidationResult.Success!
            : new ValidationResult(ErrorMessage ?? "The field is required.");
    }
}