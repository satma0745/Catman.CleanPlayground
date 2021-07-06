namespace Catman.CleanPlayground.Application.UseCases.Authentication
{
    using System.Threading.Tasks;
    using Catman.CleanPlayground.Application.UseCases.Authentication.AuthenticateUser;
    using Catman.CleanPlayground.Application.UseCases.Authentication.GetCurrentUser;
    using Catman.CleanPlayground.Application.UseCases.Common.RequestHandler;
    using Catman.CleanPlayground.Application.UseCases.Common.Response;

    internal class AuthenticationService : IAuthenticationService
    {
        private readonly IRequestHandler<AuthenticateUserRequest, string> _authenticateUserRequestHandler;
        private readonly IRequestHandler<GetCurrentUserRequest, CurrentUserResource> _getCurrentUserRequestHandler;

        public AuthenticationService(
            IRequestHandler<AuthenticateUserRequest, string> authenticateUserRequestHandler,
            IRequestHandler<GetCurrentUserRequest, CurrentUserResource> getCurrentUserRequestHandler)
        {
            _authenticateUserRequestHandler = authenticateUserRequestHandler;
            _getCurrentUserRequestHandler = getCurrentUserRequestHandler;
        }
        
        public Task<IResponse<string>> AuthenticateUserAsync(AuthenticateUserRequest request) =>
            _authenticateUserRequestHandler.PerformAsync(request);

        public Task<IResponse<CurrentUserResource>> GetCurrentUserAsync(GetCurrentUserRequest request) =>
            _getCurrentUserRequestHandler.PerformAsync(request);
    }
}
