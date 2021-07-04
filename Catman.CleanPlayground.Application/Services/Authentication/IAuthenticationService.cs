namespace Catman.CleanPlayground.Application.Services.Authentication
{
    using System.Threading.Tasks;
    using Catman.CleanPlayground.Application.Services.Authentication.Requests;
    using Catman.CleanPlayground.Application.Services.Common.Response;

    public interface IAuthenticationService
    {
        public Task<OperationResult<string>> AuthenticateUserAsync(
            AuthenticateUserRequest request,
            string authenticationToken = default);
    }
}
