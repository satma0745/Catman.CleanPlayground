namespace Catman.CleanPlayground.Application.Services.Authentication
{
    using System.Threading.Tasks;
    using Catman.CleanPlayground.Application.Services.Authentication.AuthenticateUser;
    using Catman.CleanPlayground.Application.Services.Authentication.GetCurrentUser;
    using Catman.CleanPlayground.Application.Services.Common.Response;

    public interface IAuthenticationService
    {
        public Task<OperationResult<string>> AuthenticateUserAsync(AuthenticateUserRequest request);

        public Task<OperationResult<CurrentUserResource>> GetCurrentUserAsync(GetCurrentUserRequest request);
    }
}
