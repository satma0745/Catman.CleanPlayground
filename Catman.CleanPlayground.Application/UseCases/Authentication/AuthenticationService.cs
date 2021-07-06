namespace Catman.CleanPlayground.Application.UseCases.Authentication
{
    using System.Threading.Tasks;
    using Catman.CleanPlayground.Application.UseCases.Authentication.AuthenticateUser;
    using Catman.CleanPlayground.Application.UseCases.Authentication.GetCurrentUser;
    using Catman.CleanPlayground.Application.UseCases.Common.Operation;
    using Catman.CleanPlayground.Application.UseCases.Common.Response;

    internal class AuthenticationService : IAuthenticationService
    {
        private readonly IOperation<AuthenticateUserRequest, string> _authenticateUserOperation;
        private readonly IOperation<GetCurrentUserRequest, CurrentUserResource> _getCurrentUserOperation;

        public AuthenticationService(
            IOperation<AuthenticateUserRequest, string> authenticateUserOperation,
            IOperation<GetCurrentUserRequest, CurrentUserResource> getCurrentUserOperation)
        {
            _authenticateUserOperation = authenticateUserOperation;
            _getCurrentUserOperation = getCurrentUserOperation;
        }
        
        public Task<OperationResult<string>> AuthenticateUserAsync(AuthenticateUserRequest request) =>
            _authenticateUserOperation.PerformAsync(request);

        public Task<OperationResult<CurrentUserResource>> GetCurrentUserAsync(GetCurrentUserRequest request) =>
            _getCurrentUserOperation.PerformAsync(request);
    }
}
