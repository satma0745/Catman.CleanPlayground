namespace Catman.CleanPlayground.Application.Services.Authentication
{
    using System.Threading.Tasks;
    using Catman.CleanPlayground.Application.Services.Authentication.Operations;
    using Catman.CleanPlayground.Application.Services.Authentication.Requests;
    using Catman.CleanPlayground.Application.Services.Common.Response;

    internal class AuthenticationService : IAuthenticationService
    {
        private readonly AuthenticateUserOperationHandler _authenticateUserOperationHandler;

        public AuthenticationService(AuthenticateUserOperationHandler authenticateUserOperationHandler)
        {
            _authenticateUserOperationHandler = authenticateUserOperationHandler;
        }
        
        public Task<OperationResult<string>> AuthenticateUserAsync(AuthenticateUserRequest request) =>
            _authenticateUserOperationHandler.HandleAsync(request);
    }
}
