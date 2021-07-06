namespace Catman.CleanPlayground.Application.UseCases.Common.Operation.Handler
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Catman.CleanPlayground.Application.Session;
    using Catman.CleanPlayground.Application.UseCases.Common.Request;
    using Catman.CleanPlayground.Application.UseCases.Common.Response;
    using Catman.CleanPlayground.Application.UseCases.Common.Response.Errors;
    using FluentValidation;

    internal abstract class OperationHandlerBase<TRequest, TResource> : IOperation<TRequest, TResource>
        where TRequest : RequestBase
    {
        private readonly IEnumerable<IValidator<TRequest>> _requestValidators;
        private readonly ISessionManager _sessionManager;
        
        protected virtual bool RequireAuthorizedUser => false;
        
        protected ISession Session { get; private set; }

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
                var validationResult = await RequestValidator.ValidateRequestAsync(request, _requestValidators);
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
                Session = sessionGenerationResult.Session;
                
                return await HandleRequestAsync(request);
            }
            catch (Exception exception)
            {
                var fatalError = new FatalError(exception);
                return new OperationResult<TResource>(fatalError);
            }
        }

        protected abstract Task<OperationResult<TResource>> HandleRequestAsync(TRequest request);

        protected OperationResult<TResource> ValidationFailed(string propertyName, string errorMessage)
        {
            var validationMessages = new Dictionary<string, string>
            {
                {propertyName, errorMessage}
            };
            var validationError = new ValidationError(validationMessages);
            return new OperationResult<TResource>(validationError);
        }

        protected OperationResult<TResource> AccessViolation(string message)
        {
            var accessViolationError = new AccessViolationError(message);
            return new OperationResult<TResource>(accessViolationError);
        }
        
        protected OperationResult<TResource> NotFound(string message)
        {
            var notFoundError = new NotFoundError(message);
            return new OperationResult<TResource>(notFoundError);
        }

        protected OperationResult<TResource> Success(TResource resource) =>
            new OperationResult<TResource>(resource);

        protected OperationResult<BlankResource> Success() =>
            new OperationResult<BlankResource>(new BlankResource());
    }
}
