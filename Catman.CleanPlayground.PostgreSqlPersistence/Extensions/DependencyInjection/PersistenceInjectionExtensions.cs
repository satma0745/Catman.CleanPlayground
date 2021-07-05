namespace Catman.CleanPlayground.PostgreSqlPersistence.Extensions.DependencyInjection
{
    using System.Reflection;
    using Catman.CleanPlayground.Application.Persistence.Repositories;
    using Catman.CleanPlayground.PostgreSqlPersistence.Context;
    using Catman.CleanPlayground.PostgreSqlPersistence.Extensions.Configuration;
    using Catman.CleanPlayground.PostgreSqlPersistence.Repositories;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class PersistenceInjectionExtensions
    {
        public static IServiceCollection AddPostgreSqlPersistence(
            this IServiceCollection services,
            IConfiguration configuration) =>
            services
                .AddDatabaseContext(configuration)
                .AddRepositories();

        private static IServiceCollection AddRepositories(this IServiceCollection services) =>
            services.AddScoped<IUserRepository, UserRepository>();

        private static IServiceCollection AddDatabaseContext(
            this IServiceCollection services,
            IConfiguration configuration) =>
            services.AddDbContext<DatabaseContext>(contextOptions =>
                contextOptions.UseNpgsql(
                    configuration.GetDatabaseConnectionString(),
                    npgsqlOptions => npgsqlOptions.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName)));
    }
}
