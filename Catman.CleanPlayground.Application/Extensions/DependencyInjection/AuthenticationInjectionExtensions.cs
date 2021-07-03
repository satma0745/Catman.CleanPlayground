namespace Catman.CleanPlayground.Application.Extensions.DependencyInjection
{
    using Catman.CleanPlayground.Application.Authentication;
    using Microsoft.Extensions.DependencyInjection;

    internal static class AuthenticationInjectionExtensions
    {
        public static IServiceCollection AddTokenAuthentication(this IServiceCollection services) =>
            services.AddScoped<TokenManager>();
    }
}
