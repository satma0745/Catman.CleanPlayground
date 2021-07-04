namespace Catman.CleanPlayground.JwtAuthentication.Extensions.DependencyInjection
{
    using Catman.CleanPlayground.Application.Session;
    using Catman.CleanPlayground.JwtAuthentication.Configuration;
    using Catman.CleanPlayground.JwtAuthentication.Extensions.Configuration;
    using Catman.CleanPlayground.JwtAuthentication.Session.Manager;
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
                    Algorithm = new HMACSHA512Algorithm()
                };
            });

        private static IServiceCollection AddSessionManager(this IServiceCollection services) =>
            services
                .AddScoped<ISessionManager, SessionManager>()
                .AddScoped<SessionTokenGenerator>()
                .AddScoped<SessionGenerator>();
    }
}
