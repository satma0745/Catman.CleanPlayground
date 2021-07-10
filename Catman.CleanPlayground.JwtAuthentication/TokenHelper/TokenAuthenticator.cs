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
        private readonly IUnitOfWork _work;
        private readonly JwtAuthenticationConfiguration _configuration;
        private readonly JwtBuilder _jwtBuilder;

        public TokenAuthenticator(JwtAuthenticationConfiguration configuration, IUnitOfWork work)
        {
            _work = work;
            _configuration = configuration;
            
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
                
                var token = authorizationToken.NormalizeToken(_configuration.TokenPrefixes);
                var tokenPayload = _jwtBuilder.GetPayload(token);

                if (tokenPayload.Version != _configuration.TokenVersion)
                {
                    return new TokenAuthenticationResult("Token version mismatch.");
                }
                
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
