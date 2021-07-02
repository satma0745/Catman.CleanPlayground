namespace Catman.CleanPlayground.Application.Extensions.DependencyInjection
{
    using Catman.CleanPlayground.Application.Services.Users;
    using Microsoft.Extensions.DependencyInjection;

    public static class ApplicationInjectionExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services) =>
            services
                .AddScoped<IUserService, UserService>()
                .AddUserServiceOperations();
    }
}
