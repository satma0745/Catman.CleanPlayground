namespace Catman.CleanPlayground.JwtAuthentication.Extensions.DependencyInjection
{
    using Catman.CleanPlayground.Application.Extensions.Configuration;
    using Catman.CleanPlayground.Application.Helpers.AuthorizationToken;
    using Catman.CleanPlayground.JwtAuthentication.Configuration;
    using Catman.CleanPlayground.JwtAuthentication.Extensions.Configuration;
    using Catman.CleanPlayground.JwtAuthentication.TokenHelper;
    using JWT.Algorithms;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class JwtAuthenticationInjectionExtensions
    {
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services) =>
            services
                .AddJwtAuthenticationConfiguration()
                .AddSessionManager();

        private static IServiceCollection AddJwtAuthenticationConfiguration(this IServiceCollection services) =>
            services.AddScoped(serviceProvider =>
            {
                var configuration = serviceProvider.GetService<IConfiguration>();

                return new JwtAuthenticationConfiguration
                {
                    SecretKey = configuration.GetAuthSecret(),
                    TokenLifetimeInDays = configuration.GetAuthTokenLifetime(),
                    Algorithm = new HMACSHA512Algorithm(),
                    TokenPrefixes = new[] {"Bearer"},
                    TokenVersion = 1
                };
            });

        private static IServiceCollection AddSessionManager(this IServiceCollection services) =>
            services
                .AddScoped<ITokenHelper, TokenHelper>()
                .AddScoped<TokenGenerator>()
                .AddScoped<TokenAuthenticator>();
    }
}
