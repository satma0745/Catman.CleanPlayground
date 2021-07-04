namespace Catman.CleanPlayground.PostgreSqlPersistence.Extensions.Configuration
{
    using Catman.CleanPlayground.Application.Extensions.Configuration;
    using Microsoft.Extensions.Configuration;

    internal static class ConfigurationExtensions
    {
        public static string GetDatabaseConnectionString(this IConfiguration configuration) =>
            configuration.Get("CATMAN_CLEAN_PLAYGROUND_DB_CONNECTION_STRING", "Database connection string required.");
    }
}
