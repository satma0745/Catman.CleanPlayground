namespace Catman.CleanPlayground.Extensions.DependencyInjection
{
    using Catman.CleanPlayground.Application.Extensions.DependencyInjection;
    using Catman.CleanPlayground.Persistence.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection;

    internal static class AutoMapperInjectionExtensions
    {
        public static IServiceCollection AddMappings(this IServiceCollection services)
        {
            var persistenceAssembly = typeof(PersistenceInjectionExtensions).Assembly;
            var applicationAssembly = typeof(ApplicationInjectionExtensions).Assembly;

            return services.AddAutoMapper(persistenceAssembly, applicationAssembly);
        }
    }
}
