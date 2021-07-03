namespace Catman.CleanPlayground.Application.Services.Authentication
{
    using System.Threading.Tasks;
    using Catman.CleanPlayground.Application.Services.Authentication.Models;
    using Catman.CleanPlayground.Application.Services.Common.Response;

    public interface IAuthenticationService
    {
        public Task<OperationResult<string>> AuthenticateUserAsync(UserCredentialsModel credentialsModel);
    }
}
