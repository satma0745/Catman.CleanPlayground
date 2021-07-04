namespace Catman.CleanPlayground.Application.Services.Common.Operation
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Catman.CleanPlayground.Application.Services.Common.Request;
    using Catman.CleanPlayground.Application.Services.Common.Response;
    using Catman.CleanPlayground.Application.Services.Common.Response.Errors;
    using Catman.CleanPlayground.Application.Session;
    using FluentValidation;
    using FluentValidation.Results;

    internal abstract class OperationHandlerBase<TRequest, TResource> : IOperation<TRequest, TResource>
        where TRequest : RequestBase
    {
        private readonly IEnumerable<IValidator<TRequest>> _requestValidators;
        private readonly ISessionManager _sessionManager;
        
        protected virtual bool RequireAuthorizedUser => false;

        protected OperationHandlerBase(
            IEnumerable<IValidator<TRequest>> requestValidators,
            ISessionManager sessionManager)
        {
            _requestValidators = requestValidators;
            _sessionManager = sessionManager;
        }
        
        public async Task<OperationResult<TResource>> PerformAsync(TRequest request)
        {
            try
            {
                var validationResult = await ValidateRequestAsync(request);
                if (!validationResult.IsValid)
                {
                    var validationError = new ValidationError(validationResult);
                    return new OperationResult<TResource>(validationError);
                }

                var authorizationToken = request.AuthorizationToken;
                var sessionGenerationResult = await _sessionManager.RestoreSessionAsync(authorizationToken);

                if (!sessionGenerationResult.Success && RequireAuthorizedUser)
                {
                    var authenticationError = new AuthenticationError(sessionGenerationResult.ValidationError);
                    return new OperationResult<TResource>(authenticationError);
                }

                var operationParams = new OperationParameters<TRequest>
                {
                    Session = sessionGenerationResult.Session,
                    Request = request
                };
                return await HandleRequestAsync(operationParams);
            }
            catch (Exception exception)
            {
                var fatalError = new FatalError(exception);
                return new OperationResult<TResource>(fatalError);
            }
        }

        protected abstract Task<OperationResult<TResource>> HandleRequestAsync(
            OperationParameters<TRequest> operationParameters);

        private async Task<ValidationResult> ValidateRequestAsync(TRequest request)
        {
            foreach (var requestValidator in _requestValidators)
            {
                var validationResult = await requestValidator.ValidateAsync(request);
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
