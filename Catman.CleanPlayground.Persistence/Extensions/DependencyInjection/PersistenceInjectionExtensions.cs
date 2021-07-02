namespace Catman.CleanPlayground.Persistence.Extensions.DependencyInjection
{
    using Catman.CleanPlayground.Application.Persistence.Users;
    using Catman.CleanPlayground.Persistence.Repositories;
    using Microsoft.Extensions.DependencyInjection;

    public static class PersistenceInjectionExtensions
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services) =>
            services.AddSingleton<IUserRepository, InMemoryUserRepository>();
    }
}
