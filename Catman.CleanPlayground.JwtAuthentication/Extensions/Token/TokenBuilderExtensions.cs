namespace Catman.CleanPlayground.JwtAuthentication.Extensions.Token
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Catman.CleanPlayground.JwtAuthentication.TokenHelper;
    using JWT.Builder;

    internal static class TokenBuilderExtensions
    {
        public static JwtBuilder AddExpirationDate(this JwtBuilder tokenBuilder, int tokenLifetimeInDays) =>
            tokenBuilder.AddClaim("exp", DateTimeOffset.UtcNow.AddDays(tokenLifetimeInDays).ToUnixTimeSeconds());

        public static JwtBuilder AddUserId(this JwtBuilder tokenBuilder, Guid userId) =>
            tokenBuilder.AddClaim("sub", userId);
            

        public static JwtBuilder AddVersion(this JwtBuilder tokenBuilder, byte version) =>
            tokenBuilder.AddClaim("ver", version);

        public static TokenPayload GetPayload(this JwtBuilder tokenBuilder, string token)
        {
            var claims = tokenBuilder.Decode<IDictionary<string, object>>(token);

            return new TokenPayload
            {
                UserId = Guid.Parse((string) claims["sub"]),
                Version = (byte) (long) claims["ver"]
            };
        }

        public static string NormalizeToken(this string authorizationToken, IEnumerable<string> supportedPrefixes) =>
            supportedPrefixes.Aggregate(authorizationToken, (token, prefix) =>
                token.StartsWith(prefix)
                    ? token.Replace(prefix, string.Empty).TrimStart()
                    : token);
    }
}
