namespace Catman.CleanPlayground.JwtAuthentication.Extensions.Configuration
{
    using Catman.CleanPlayground.Application.Extensions.Configuration;
    using Microsoft.Extensions.Configuration;

    internal static class ConfigurationExtensions
    {
        public static string GetAuthSecret(this IConfiguration configuration) =>
            configuration.Get("CATMAN_CLEAN_PLAYGROUND_AUTH_SECRET", "Authentication secret key required.");

        public static int GetAuthTokenLifetime(this IConfiguration configuration) =>
            configuration.Get(
                "CATMAN_CLEAN_PLAYGROUND_TOKEN_LIFETIME_DAYS",
                "Authentication token lifetime required.",
                int.Parse);
    }
}
