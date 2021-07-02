namespace Catman.CleanPlayground.Extensions.DependencyInjection
{
    using Catman.CleanPlayground.Presentation;
    using Microsoft.Extensions.DependencyInjection;

    internal static class ConsoleInjectionExtensions
    {
        public static IServiceCollection AddConsole(this IServiceCollection services) =>
            services.AddScoped<UsersPresentation>();
    }
}
