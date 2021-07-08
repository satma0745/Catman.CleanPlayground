namespace Catman.CleanPlayground.Application.Helpers.AuthorizationToken
{
    using System;
    using System.Threading.Tasks;

    public interface ITokenHelper
    {
        string GenerateToken(Guid userId);

        Task<ITokenAuthenticationResult> AuthenticateTokenAsync(string authorizationToken);
    }
}
