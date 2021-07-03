namespace Catman.CleanPlayground.Application.Extensions.Configuration
{
    using System;
    using Microsoft.Extensions.Configuration;

    public static class CommonConfigurationExtensions
    {
        public static string Get(this IConfiguration configuration, string key, string requiredErrorMessage) =>
            configuration[key] ?? throw new Exception(requiredErrorMessage);
        
        public static TValue Get<TValue>(
            this IConfiguration configuration,
            string key,
            string requiredErrorMessage,
            Func<string, TValue> parse)
        {
            var valueString = configuration.Get(key, requiredErrorMessage);
            return parse(valueString);
        }
    }
}
