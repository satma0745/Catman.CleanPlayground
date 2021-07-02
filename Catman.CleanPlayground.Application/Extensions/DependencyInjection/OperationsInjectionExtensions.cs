namespace Catman.CleanPlayground.Application.Extensions.DependencyInjection
{
    using Catman.CleanPlayground.Application.Services.Users.Operations;
    using Microsoft.Extensions.DependencyInjection;

    internal static class OperationsInjectionExtensions
    {
        public static IServiceCollection AddUserServiceOperations(this IServiceCollection services) =>
            services
                .AddScoped<GetUsersOperationHandler>()
                .AddScoped<RegisterUserOperationHandler>()
                .AddScoped<UpdateUserOperationHandler>()
                .AddScoped<DeleteUserOperationHandler>();
    }
}
