namespace Catman.CleanPlayground.Application.Extensions.DependencyInjection
{
    using System.Collections.Generic;
    using Catman.CleanPlayground.Application.Services.Authentication;
    using Catman.CleanPlayground.Application.Services.Authentication.Operations;
    using Catman.CleanPlayground.Application.Services.Authentication.Requests;
    using Catman.CleanPlayground.Application.Services.Authentication.Resources;
    using Catman.CleanPlayground.Application.Services.Common.Operation;
    using Catman.CleanPlayground.Application.Services.Common.Response;
    using Catman.CleanPlayground.Application.Services.Users;
    using Catman.CleanPlayground.Application.Services.Users.Operations;
    using Catman.CleanPlayground.Application.Services.Users.Requests;
    using Catman.CleanPlayground.Application.Services.Users.Resources;
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
                .AddScoped<IOperation<GetUsersRequest, ICollection<UserResource>>, GetUsersOperationHandler>()
                .AddScoped<IOperation<RegisterUserRequest, BlankResource>, RegisterUserOperationHandler>()
                .AddScoped<IOperation<UpdateUserRequest, BlankResource>, UpdateUserOperationHandler>()
                .AddScoped<IOperation<DeleteUserRequest, BlankResource>, DeleteUserOperationHandler>();

        private static IServiceCollection AddAuthenticationService(this IServiceCollection services) =>
            services
                .AddScoped<IAuthenticationService, AuthenticationService>()
                .AddScoped<IOperation<AuthenticateUserRequest, string>, AuthenticateUserOperationHandler>()
                .AddScoped<IOperation<GetCurrentUserRequest, CurrentUserResource>, GetCurrentUserOperationHandler>();
    }
}
