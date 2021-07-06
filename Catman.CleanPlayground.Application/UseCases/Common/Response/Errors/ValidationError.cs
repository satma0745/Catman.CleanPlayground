namespace Catman.CleanPlayground.Application.UseCases.Common.Response.Errors
{
    using System.Collections.Generic;
    using Catman.CleanPlayground.Application.Extensions.Validation;
    using FluentValidation.Results;

    public class ValidationError : Error
    {
        public IDictionary<string, string> ValidationErrors { get; }

        public ValidationError(IDictionary<string, string> validationErrors)
            : base("Validation errors occurred")
        {
            ValidationErrors = validationErrors;
        }

        public ValidationError(ValidationResult validationResult)
            : this(validationResult.GetValidationErrors())
        {
        }
    }
}
