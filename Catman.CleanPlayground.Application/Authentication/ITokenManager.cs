namespace Catman.CleanPlayground.Application.Authentication
{
    using System;

    public interface ITokenManager
    {
        string GenerateToken(Guid userId);

        ITokenAuthenticationResult AuthenticateToken(string token);
    }
}
