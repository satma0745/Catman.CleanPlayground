namespace Catman.CleanPlayground.Application.Services.Common.Operation.Handler
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using FluentValidation;
    using FluentValidation.Results;

    internal static class RequestValidator
    {
        public static async Task<ValidationResult> ValidateRequestAsync<TRequest>(
            TRequest requestToValidate,
            IEnumerable<IValidator<TRequest>> requestValidators)
        {
            foreach (var requestValidator in requestValidators)
            {
                var validationResult = await requestValidator.ValidateAsync(requestToValidate);
                if (!validationResult.IsValid)
                {
                    return validationResult;
                }
            }

            var validationPassed = new ValidationResult();
            return validationPassed;
        }
    }
}
