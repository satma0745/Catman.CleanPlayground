namespace Catman.CleanPlayground.Application.Services.Common.Response.Errors
{
    using System.Collections.Generic;
    using System.Linq;
    using FluentValidation.Results;

    public class ValidationError : Error
    {
        public IDictionary<string, string> ValidationErrors { get; }

        public ValidationError(IDictionary<string, string> validationErrors)
            : base("Validation errors occurred")
        {
            ValidationErrors = validationErrors;
        }

        public ValidationError(IEnumerable<ValidationFailure> validationFailures)
            : this(validationFailures.ToDictionary(failure => failure.PropertyName, failure => failure.ErrorMessage))
        {
        }
    }
}
