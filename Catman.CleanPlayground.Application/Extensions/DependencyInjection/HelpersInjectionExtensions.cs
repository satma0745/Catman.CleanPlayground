namespace Catman.CleanPlayground.Application.Extensions.DependencyInjection
{
    using Catman.CleanPlayground.Application.Helpers.Password;
    using Catman.CleanPlayground.Application.Helpers.Session;
    using Microsoft.Extensions.DependencyInjection;

    internal static class HelpersInjectionExtensions
    {
        public static IServiceCollection AddHelpers(this IServiceCollection services) =>
            services
                .AddScoped<IPasswordHelper, PasswordHelper>()
                .AddScoped<ISessionManager, SessionManager>();
    }
}
