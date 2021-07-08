namespace Catman.CleanPlayground.Application.UseCases.Common.RequestHandling
{
    using System.Threading.Tasks;
    using Catman.CleanPlayground.Application.Helpers.AuthorizationToken;
    using Catman.CleanPlayground.Application.Helpers.Session;
    using Catman.CleanPlayground.Application.UseCases.Common.Response;
    using Catman.CleanPlayground.Application.UseCases.Common.Response.Errors;
    using MediatR;

    internal class SessionRequestPipelineBehavior<TAnyRequest, TResponse> : PipelineBehaviorBase<TAnyRequest, TResponse>
    {
        private readonly ITokenHelper _tokenHelper;
        private readonly ISessionManager _sessionManager;

        public SessionRequestPipelineBehavior(ITokenHelper tokenHelper, ISessionManager sessionManager)
        {
            _tokenHelper = tokenHelper;
            _sessionManager = sessionManager;
        }
        
        protected override async Task<IResponse<TResource>> HandleAsync<TRequest, TResource>(
            TRequest request,
            RequestHandlerDelegate<IResponse<TResource>> next)
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

            return await next();
        }
    }
}
