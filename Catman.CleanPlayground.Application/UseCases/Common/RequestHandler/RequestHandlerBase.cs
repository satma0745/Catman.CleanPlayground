namespace Catman.CleanPlayground.Application.UseCases.Common.RequestHandler
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Catman.CleanPlayground.Application.Session;
    using Catman.CleanPlayground.Application.UseCases.Common.Request;
    using Catman.CleanPlayground.Application.UseCases.Common.Response;
    using Catman.CleanPlayground.Application.UseCases.Common.Response.Errors;
    using FluentValidation;
    using MediatR;

    internal abstract class RequestHandlerBase<TRequest, TResource> : IRequestHandler<TRequest, IResponse<TResource>>
        where TRequest : RequestBase<TResource>
    {
        private readonly IEnumerable<IValidator<TRequest>> _requestValidators;
        private readonly ISessionManager _sessionManager;
        
        protected virtual bool RequireAuthorizedUser => false;
        
        protected ISession Session { get; private set; }

        protected RequestHandlerBase(
            IEnumerable<IValidator<TRequest>> requestValidators,
            ISessionManager sessionManager)
        {
            _requestValidators = requestValidators;
            _sessionManager = sessionManager;
        }

        public async Task<IResponse<TResource>> Handle(TRequest request, CancellationToken _)
        {
            try
            {
                var validationResult = await RequestValidator.ValidateRequestAsync(request, _requestValidators);
                if (!validationResult.IsValid)
                {
                    var validationError = new ValidationError(validationResult);
                    return new Response<TResource>(validationError);
                }

                var authorizationToken = request.AuthorizationToken;
                var sessionGenerationResult = await _sessionManager.RestoreSessionAsync(authorizationToken);
                if (!sessionGenerationResult.Success && RequireAuthorizedUser)
                {
                    var authenticationError = new AuthenticationError(sessionGenerationResult.ValidationError);
                    return new Response<TResource>(authenticationError);
                }
                Session = sessionGenerationResult.Session;
                
                return await HandleAsync(request);
            }
            catch (Exception exception)
            {
                var fatalError = new FatalError(exception);
                return new Response<TResource>(fatalError);
            }
        }

        protected abstract Task<Response<TResource>> HandleAsync(TRequest request);

        protected Response<TResource> ValidationFailed(string propertyName, string errorMessage)
        {
            var validationMessages = new Dictionary<string, string>
            {
                {propertyName, errorMessage}
            };
            var validationError = new ValidationError(validationMessages);
            return new Response<TResource>(validationError);
        }

        protected Response<TResource> AccessViolation(string message)
        {
            var accessViolationError = new AccessViolationError(message);
            return new Response<TResource>(accessViolationError);
        }
        
        protected Response<TResource> NotFound(string message)
        {
            var notFoundError = new NotFoundError(message);
            return new Response<TResource>(notFoundError);
        }

        protected Response<TResource> Success(TResource resource) =>
            new Response<TResource>(resource);

        protected Response<BlankResource> Success() =>
            new Response<BlankResource>(new BlankResource());
    }
}
