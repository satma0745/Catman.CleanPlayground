namespace Catman.CleanPlayground.Application.Extensions.DependencyInjection
{
    using Microsoft.Extensions.DependencyInjection;

    public static class ApplicationInjectionExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services) =>
            services
                .AddUseCases()
                .AddHelpers()
                .AddValidators();
    }
}
