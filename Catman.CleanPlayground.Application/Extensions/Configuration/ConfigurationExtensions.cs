namespace Catman.CleanPlayground.Application.Extensions.Configuration
{
    using Microsoft.Extensions.Configuration;

    public static class ConfigurationExtensions
    {
        public static string GetApplicationTitle(this IConfiguration configuration) =>
            configuration.Get("Application:Title", "Application title required");
        
        public static string GetApplicationVersion(this IConfiguration configuration) =>
            configuration.Get("Application:Version", "Application version required");
    }
}
