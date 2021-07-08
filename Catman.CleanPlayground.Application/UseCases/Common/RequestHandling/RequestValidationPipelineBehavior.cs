namespace Catman.CleanPlayground.Application.UseCases.Common.RequestHandling
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Catman.CleanPlayground.Application.UseCases.Common.Response;
    using Catman.CleanPlayground.Application.UseCases.Common.Response.Errors;
    using FluentValidation;
    using FluentValidation.Results;
    using MediatR;

    internal class RequestValidationPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private static bool IsSupportedResponseType =>
            typeof(TResponse).IsInterface &&
            typeof(TResponse).IsGenericType &&
            typeof(TResponse).GetGenericTypeDefinition() == typeof(IResponse<>);
        
        private readonly IEnumerable<IValidator<TRequest>> _requestValidators;

        public RequestValidationPipelineBehavior(IEnumerable<IValidator<TRequest>> requestValidators)
        {
            _requestValidators = requestValidators;
        }
        
        public async Task<TResponse> Handle(
            TRequest request,
            CancellationToken _,
            RequestHandlerDelegate<TResponse> next)
        {
            if (!IsSupportedResponseType)
            {
                throw new Exception("Unsupported request result type.");
            }

            var validationResult = await ValidateRequestAsync(request);
            if (!validationResult.IsValid)
            {
                return GenerateValidationFailureResponse(validationResult);
            }

            return await next();
        }

        private async Task<ValidationResult> ValidateRequestAsync(TRequest requestToValidate)
        {
            foreach (var requestValidator in _requestValidators)
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

        private static TResponse GenerateValidationFailureResponse(ValidationResult validationResult)
        {
            var resourceType = typeof(TResponse).GetGenericArguments()[0];
            var responseType = typeof(Response<>).MakeGenericType(resourceType);
            
            var validationError = new ValidationError(validationResult);

            return (TResponse) Activator.CreateInstance(responseType, validationError);
        }
    }
}
