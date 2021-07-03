namespace Catman.CleanPlayground.JwtAuthentication.Extensions.Token
{
    using System;
    using System.Collections.Generic;
    using Catman.CleanPlayground.JwtAuthentication.Token;
    using JWT.Builder;

    internal static class TokenBuilderExtensions
    {
        public static JwtBuilder AddExpirationDate(this JwtBuilder tokenBuilder, int tokenLifetimeInDays) =>
            tokenBuilder.AddClaim("exp", DateTimeOffset.UtcNow.AddDays(tokenLifetimeInDays).ToUnixTimeSeconds());

        public static JwtBuilder AddUserId(this JwtBuilder tokenBuilder, Guid userId) =>
            tokenBuilder.AddClaim("sub", userId);

        public static TokenPayload GetPayload(this JwtBuilder tokenBuilder, string token)
        {
            var claims = tokenBuilder.Decode<IDictionary<string, object>>(token);

            return new TokenPayload
            {
                UserId = Guid.Parse((string) claims["sub"])
            };
        }
    }
}
