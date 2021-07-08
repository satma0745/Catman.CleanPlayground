namespace Catman.CleanPlayground.JwtAuthentication.TokenHelper
{
    using System;
    using System.Threading.Tasks;
    using Catman.CleanPlayground.Application.Helpers.AuthorizationToken;

    internal class TokenHelper : ITokenHelper
    {
        private readonly TokenGenerator _tokenGenerator;
        private readonly TokenAuthenticator _tokenAuthenticator;

        public TokenHelper(TokenGenerator tokenGenerator, TokenAuthenticator tokenAuthenticator)
        {
            _tokenGenerator = tokenGenerator;
            _tokenAuthenticator = tokenAuthenticator;
        }

        public string GenerateToken(Guid userId) =>
            _tokenGenerator.GenerateToken(userId);

        public Task<ITokenAuthenticationResult> AuthenticateTokenAsync(string authorizationToken) =>
            _tokenAuthenticator.AuthenticateTokenAsync(authorizationToken);
    }
}
