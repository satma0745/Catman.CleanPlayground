namespace Catman.CleanPlayground.JwtAuthentication.Extensions.DependencyInjection
{
    using System;
    using Catman.CleanPlayground.Application.Authentication;
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

                var secretKey = configuration["CATMAN_CLEAN_PLAYGROUND_AUTH_SECRET"] ??
                                throw new Exception("Authentication secret key required.");

                var tokenLifetimeString = configuration["CATMAN_CLEAN_PLAYGROUND_TOKEN_LIFETIME_DAYS"] ??
                                          throw new Exception("Authentication token lifetime required");
                
                return new JwtAuthenticationConfiguration
                {
                    SecretKey = secretKey,
                    TokenLifetimeInDays = int.Parse(tokenLifetimeString),
                    Algorithm = new HMACSHA512Algorithm()
                };
            });
    }
}
