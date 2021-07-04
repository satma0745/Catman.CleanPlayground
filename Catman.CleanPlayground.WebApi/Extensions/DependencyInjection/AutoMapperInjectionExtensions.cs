namespace Catman.CleanPlayground.WebApi.Extensions.DependencyInjection
{
    using Catman.CleanPlayground.Application.Extensions.DependencyInjection;
    using Catman.CleanPlayground.JwtAuthentication.Extensions.DependencyInjection;
    using Catman.CleanPlayground.PostgreSqlPersistence.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection;

    internal static class AutoMapperInjectionExtensions
    {
        public static IServiceCollection AddMappings(this IServiceCollection services)
        {
            var persistenceAssembly = typeof(PersistenceInjectionExtensions).Assembly;
            var applicationAssembly = typeof(ApplicationInjectionExtensions).Assembly;
            var jwtAuthenticationAssembly = typeof(JwtAuthenticationInjectionExtensions).Assembly;
            var webApiAssembly = typeof(Program).Assembly;

            return services.AddAutoMapper(
                persistenceAssembly,
                applicationAssembly,
                jwtAuthenticationAssembly,
                webApiAssembly);
        }
    }
}
