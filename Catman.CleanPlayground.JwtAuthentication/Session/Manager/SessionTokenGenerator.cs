namespace Catman.CleanPlayground.JwtAuthentication.Session.Manager
{
    using System;
    using Catman.CleanPlayground.JwtAuthentication.Configuration;
    using Catman.CleanPlayground.JwtAuthentication.Extensions.Token;
    using JWT.Builder;

    internal class SessionTokenGenerator
    {
        private readonly JwtAuthenticationConfiguration _configuration;
        private readonly JwtBuilder _jwtBuilder;

        public SessionTokenGenerator(JwtAuthenticationConfiguration configuration)
        {
            _configuration = configuration;
            
            _jwtBuilder = JwtBuilder.Create()
                .WithAlgorithm(_configuration.Algorithm)
                .WithSecret(_configuration.SecretKey);
        }

        public string GenerateToken(Guid userId) =>
            _jwtBuilder
                .AddUserId(userId)
                .AddExpirationDate(_configuration.TokenLifetimeInDays)
                .Encode();
    }
}
