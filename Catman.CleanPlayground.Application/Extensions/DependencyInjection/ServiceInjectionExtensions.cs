namespace Catman.CleanPlayground.Application.Extensions.DependencyInjection
{
    using Catman.CleanPlayground.Application.Services.Authentication;
    using Catman.CleanPlayground.Application.Services.Authentication.Operations;
    using Catman.CleanPlayground.Application.Services.Users;
    using Catman.CleanPlayground.Application.Services.Users.Operations;
    using Microsoft.Extensions.DependencyInjection;

    internal static class ServiceInjectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services) =>
            services
                .AddUserService()
                .AddAuthenticationService();

        private static IServiceCollection AddUserService(this IServiceCollection services) =>
            services
                .AddScoped<IUserService, UserService>()
                .AddScoped<GetUsersOperationHandler>()
                .AddScoped<RegisterUserOperationHandler>()
                .AddScoped<UpdateUserOperationHandler>()
                .AddScoped<DeleteUserOperationHandler>();

        private static IServiceCollection AddAuthenticationService(this IServiceCollection services) =>
            services
                .AddScoped<IAuthenticationService, AuthenticationService>()
                .AddScoped<AuthenticateUserOperationHandler>();
    }
}
