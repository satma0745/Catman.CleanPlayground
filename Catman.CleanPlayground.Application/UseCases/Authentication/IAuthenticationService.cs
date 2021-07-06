namespace Catman.CleanPlayground.Application.UseCases.Authentication
{
    using System.Threading.Tasks;
    using Catman.CleanPlayground.Application.UseCases.Authentication.AuthenticateUser;
    using Catman.CleanPlayground.Application.UseCases.Authentication.GetCurrentUser;
    using Catman.CleanPlayground.Application.UseCases.Common.Response;

    public interface IAuthenticationService
    {
        public Task<IResponse<string>> AuthenticateUserAsync(AuthenticateUserRequest request);

        public Task<IResponse<CurrentUserResource>> GetCurrentUserAsync(GetCurrentUserRequest request);
    }
}
