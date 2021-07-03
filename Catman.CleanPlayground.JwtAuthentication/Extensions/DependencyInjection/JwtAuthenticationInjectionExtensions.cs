namespace Catman.CleanPlayground.JwtAuthentication.Extensions.DependencyInjection
{
    using Catman.CleanPlayground.Application.Authentication;
    using Catman.CleanPlayground.JwtAuthentication.Extensions.Configuration;
    using Catman.CleanPlayground.JwtAuthentication.Token;
    using JWT.Algorithms;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class JwtAuthenticationInjectionExtensions
    {
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services) =>
            services
                .AddJwtAuthenticationConfiguration()
                .AddScoped<ITokenManager, JwtTokenManager>();

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
    }
}
