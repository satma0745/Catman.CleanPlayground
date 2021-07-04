namespace Catman.CleanPlayground.JwtAuthentication.Token
{
    using System;
    using Catman.CleanPlayground.Application.Authentication;
    using Catman.CleanPlayground.JwtAuthentication.Extensions.Token;
    using JWT.Builder;
    using JWT.Exceptions;

    internal class JwtTokenManager : ITokenManager
    {
        private readonly JwtAuthenticationConfiguration _configuration;
        private readonly JwtBuilder _jwtBuilder;

        public JwtTokenManager(JwtAuthenticationConfiguration configuration)
        {
            _configuration = configuration;
            
            _jwtBuilder = JwtBuilder.Create()
                .WithAlgorithm(_configuration.Algorithm)
                .WithSecret(_configuration.SecretKey)
                .MustVerifySignature();
        }

        public string GenerateToken(Guid userId) =>
            _jwtBuilder
                .AddUserId(userId)
                .AddExpirationDate(_configuration.TokenLifetimeInDays)
                .Encode();

        public ITokenAuthenticationResult AuthenticateToken(string token)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(token))
                {
                    return TokenAuthenticationResult.ValidationFailed("No authentication token provided.");
                }
                
                var payload = _jwtBuilder.GetPayload(token);
                return TokenAuthenticationResult.ValidationPassed(payload.UserId);
            }
            catch (TokenExpiredException)
            {
                return TokenAuthenticationResult.ValidationFailed("Token has expired.");
            }
            catch (SignatureVerificationException)
            {
                return TokenAuthenticationResult.ValidationFailed("A fake token provided.");
            }
        }
    }
}
