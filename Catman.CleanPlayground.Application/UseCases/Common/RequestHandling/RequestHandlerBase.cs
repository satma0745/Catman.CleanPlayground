namespace Catman.CleanPlayground.Application.UseCases.Common.RequestHandling
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Catman.CleanPlayground.Application.Helpers.AuthorizationToken;
    using Catman.CleanPlayground.Application.Helpers.Session;
    using Catman.CleanPlayground.Application.UseCases.Common.Request;
    using Catman.CleanPlayground.Application.UseCases.Common.Response;
    using Catman.CleanPlayground.Application.UseCases.Common.Response.Errors;
    using MediatR;

    internal abstract class RequestHandlerBase<TRequest, TResource> : IRequestHandler<TRequest, IResponse<TResource>>
        where TRequest : RequestBase<TResource>
    {
        private readonly ISessionManager _sessionManager;
        private readonly ITokenHelper _tokenHelper;
        
        protected virtual bool RequireAuthorizedUser => false;
        
        protected RequestHandlerBase(ISessionManager sessionManager, ITokenHelper tokenHelper)
        {
            _sessionManager = sessionManager;
            _tokenHelper = tokenHelper;
        }

        public async Task<IResponse<TResource>> Handle(TRequest request, CancellationToken _)
        {
            try
            {
                var authorizationToken = request.AuthorizationToken;
                var tokenAuthenticationResult = await _tokenHelper.AuthenticateTokenAsync(authorizationToken);
                if (!tokenAuthenticationResult.Success && RequireAuthorizedUser)
                {
                    var authenticationError = new AuthenticationError(tokenAuthenticationResult.ErrorMessage);
                    return new Response<TResource>(authenticationError);
                }
                if (tokenAuthenticationResult.Success)
                {
                    await _sessionManager.AuthorizeUserAsync(tokenAuthenticationResult.UserId);
                }
                
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
