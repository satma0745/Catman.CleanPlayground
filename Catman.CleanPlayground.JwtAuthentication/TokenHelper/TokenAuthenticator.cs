namespace Catman.CleanPlayground.JwtAuthentication.TokenHelper
{
    using System.Threading.Tasks;
    using Catman.CleanPlayground.Application.Helpers.AuthorizationToken;
    using Catman.CleanPlayground.Application.Persistence.UnitOfWork;
    using Catman.CleanPlayground.JwtAuthentication.Configuration;
    using Catman.CleanPlayground.JwtAuthentication.Extensions.Token;
    using JWT.Builder;
    using JWT.Exceptions;

    internal class TokenAuthenticator
    {
        private readonly JwtBuilder _jwtBuilder;
        private readonly IUnitOfWork _work;

        public TokenAuthenticator(JwtAuthenticationConfiguration configuration, IUnitOfWork work)
        {
            _work = work;
            
            _jwtBuilder = JwtBuilder.Create()
                .WithAlgorithm(configuration.Algorithm)
                .WithSecret(configuration.SecretKey)
                .MustVerifySignature();
        }

        public async Task<ITokenAuthenticationResult> AuthenticateTokenAsync(string authorizationToken)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(authorizationToken))
                {
                    return new TokenAuthenticationResult("No authentication token provided.");
                }
                
                var tokenPayload = _jwtBuilder.GetPayload(authorizationToken);
                if (!await _work.Users.UserExistsAsync(tokenPayload.UserId))
                {
                    return new TokenAuthenticationResult("Authorization token owner does not exist.");
                }

                return new TokenAuthenticationResult(tokenPayload.UserId);
            }
            catch (TokenExpiredException)
            {
                return new TokenAuthenticationResult("Authorization token has expired.");
            }
            catch (SignatureVerificationException)
            {
                return new TokenAuthenticationResult("A fake authorization token provided.");
            }
        }
    }
}
