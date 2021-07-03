namespace Catman.CleanPlayground.JwtAuthentication.Token
{
    using System;
    using System.Collections.Generic;
    using Catman.CleanPlayground.Application.Authentication;
    using JWT.Builder;
    using JWT.Exceptions;

    internal class JwtTokenManager : ITokenManager
    {
        private readonly JwtAuthenticationConfiguration _configuration;
        private readonly JwtBuilder _jwtBuilder;

        public JwtTokenManager(JwtAuthenticationConfiguration configuration)
        {
            _configuration = configuration;
            _jwtBuilder = JwtBuilder.Create();
        }

        public string GenerateToken(Guid userId) =>
            _jwtBuilder
                .WithAlgorithm(_configuration.Algorithm)
                .WithSecret(_configuration.SecretKey)
                .AddClaim("exp", DateTimeOffset.UtcNow.AddDays(_configuration.TokenLifetimeInDays).ToUnixTimeSeconds())
                .AddClaim("sub", userId)
                .Encode();

        public ITokenAuthenticationResult AuthenticateToken(string token)
        {
            try
            {
                var payload = _jwtBuilder
                    .WithAlgorithm(_configuration.Algorithm)
                    .WithSecret(_configuration.SecretKey)
                    .MustVerifySignature()
                    .Decode<IDictionary<string, string>>(token);

                var userId = Guid.Parse(payload["sub"]);
                
                return TokenAuthenticationResult.ValidationPassed(userId);
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
