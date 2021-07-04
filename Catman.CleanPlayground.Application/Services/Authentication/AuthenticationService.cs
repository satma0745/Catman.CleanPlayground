namespace Catman.CleanPlayground.Application.Services.Authentication
{
    using System.Threading.Tasks;
    using Catman.CleanPlayground.Application.Services.Authentication.Requests;
    using Catman.CleanPlayground.Application.Services.Common.Operation;
    using Catman.CleanPlayground.Application.Services.Common.Response;

    internal class AuthenticationService : IAuthenticationService
    {
        private readonly IOperation<AuthenticateUserRequest, string> _authenticateUserOperation;

        public AuthenticationService(IOperation<AuthenticateUserRequest, string> authenticateUserOperation)
        {
            _authenticateUserOperation = authenticateUserOperation;
        }
        
        public Task<OperationResult<string>> AuthenticateUserAsync(AuthenticateUserRequest request) =>
            _authenticateUserOperation.PerformAsync(request);
    }
}
