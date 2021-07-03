namespace Catman.CleanPlayground.Application.Services.Authentication
{
    using System.Threading.Tasks;
    using Catman.CleanPlayground.Application.Services.Authentication.Models;
    using Catman.CleanPlayground.Application.Services.Authentication.Operations;
    using Catman.CleanPlayground.Application.Services.Common.Response;

    internal class AuthenticationService : IAuthenticationService
    {
        private readonly AuthenticateUserOperationHandler _authenticateUserOperationHandler;

        public AuthenticationService(AuthenticateUserOperationHandler authenticateUserOperationHandler)
        {
            _authenticateUserOperationHandler = authenticateUserOperationHandler;
        }
        
        public Task<OperationResult<string>> AuthenticateUserAsync(UserCredentialsModel credentialsModel) =>
            _authenticateUserOperationHandler.HandleAsync(credentialsModel);
    }
}
