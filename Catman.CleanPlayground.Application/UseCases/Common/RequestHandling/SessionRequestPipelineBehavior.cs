namespace Catman.CleanPlayground.Application.UseCases.Common.RequestHandling
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;
    using Catman.CleanPlayground.Application.Helpers.AuthorizationToken;
    using Catman.CleanPlayground.Application.Helpers.Session;
    using Catman.CleanPlayground.Application.UseCases.Common.Request;
    using Catman.CleanPlayground.Application.UseCases.Common.Response;
    using Catman.CleanPlayground.Application.UseCases.Common.Response.Errors;
    using MediatR;

    internal class SessionRequestPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private static bool IsSupportedResponseType =>
            typeof(TResponse).IsInterface &&
            typeof(TResponse).IsGenericType &&
            typeof(TResponse).GetGenericTypeDefinition() == typeof(IResponse<>);
        
        private readonly ITokenHelper _tokenHelper;
        private readonly ISessionManager _sessionManager;

        public SessionRequestPipelineBehavior(ITokenHelper tokenHelper, ISessionManager sessionManager)
        {
            _tokenHelper = tokenHelper;
            _sessionManager = sessionManager;
        }
        
        public Task<TResponse> Handle(
            TRequest request,
            CancellationToken _,
            RequestHandlerDelegate<TResponse> next)
        {
            if (!IsSupportedResponseType)
            {
                throw new Exception("Unsupported request result type.");
            }

            return (Task<TResponse>) GetType()
                .GetMethod(nameof(HandleRequestAsync), BindingFlags.Instance | BindingFlags.NonPublic)!
                .MakeGenericMethod(typeof(TResponse).GenericTypeArguments.Single())
                .Invoke(this, new object[] {request, next});
        }
            
        private async Task<IResponse<TResource>> HandleRequestAsync<TResource>(
            RequestBase<TResource> request,
            RequestHandlerDelegate<IResponse<TResource>> handleAsync)
        {
            var authorizationToken = request.AuthorizationToken;
            var tokenAuthenticationResult = await _tokenHelper.AuthenticateTokenAsync(authorizationToken);
            
            // required but invalid
            if (!tokenAuthenticationResult.Success && request.RequireAuthorizedUser)
            {
                var authenticationError = new AuthenticationError(tokenAuthenticationResult.ErrorMessage);
                return new Response<TResource>(authenticationError);
            }
            
            // valid
            if (tokenAuthenticationResult.Success)
            {
                await _sessionManager.AuthorizeUserAsync(tokenAuthenticationResult.UserId);
            }

            return await handleAsync();
        }
    }
}
