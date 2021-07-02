namespace Catman.CleanPlayground.Application.Extensions.Validation
{
    using System.Collections.Generic;
    using System.Linq;
    using FluentValidation.Results;

    public static class ValidationResultExtensions
    {
        public static IDictionary<string, string> GetValidationErrors(this ValidationResult validationResult) =>
            validationResult.Errors.ToDictionary(failure => failure.PropertyName, failure => failure.ErrorMessage);
    }
}
