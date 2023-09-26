using FluentValidation.Results;

namespace eSlozka.Domain.Extensions;

public static class FluentValidationFailureHelpers
{
    public static Dictionary<string, string[]> DistinctErrorsByProperty(this ValidationResult validationResult)
    {
        ArgumentNullException.ThrowIfNull(validationResult);

        return validationResult.Errors
            .GroupBy(validationFailure =>
                    validationFailure.PropertyName,
                validationFailure => validationFailure.ErrorMessage,
                (propertyName, validationFailuresByProperty) => new
                {
                    Key = propertyName,
                    Values = validationFailuresByProperty.Distinct().ToArray()
                })
            .ToDictionary(
                group => group.Key,
                group => group.Values);
    }

    public static Dictionary<string, string[]> DistinctErrorsByProperty(this IEnumerable<ValidationResult> validationResults)
    {
        ArgumentNullException.ThrowIfNull(validationResults);

        return validationResults
            .SelectMany(
                validationResult => validationResult.Errors,
                (_, validationFailure) => validationFailure)
            .GroupBy(validationFailure =>
                    validationFailure.PropertyName,
                validationFailure => validationFailure.ErrorMessage,
                (propertyName, validationFailuresByProperty) => new
                {
                    Key = propertyName,
                    Values = validationFailuresByProperty.Distinct().ToArray()
                })
            .ToDictionary(
                group => group.Key,
                group => group.Values);
    }
}