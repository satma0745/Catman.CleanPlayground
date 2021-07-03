namespace Catman.CleanPlayground.Application.Extensions.DependencyInjection
{
    using Catman.CleanPlayground.Application.Services.Authentication.Requests;
    using Catman.CleanPlayground.Application.Services.Users.Requests;
    using FluentValidation;
    using Microsoft.Extensions.DependencyInjection;

    internal static class FluentValidatorInjectionExtensions
    {
        public static IServiceCollection AddValidators(this IServiceCollection services) =>
            services
                .AddUserValidators()
                .AddAuthenticationValidators();

        private static IServiceCollection AddUserValidators(this IServiceCollection services) =>
            services
                .AddScoped<IValidator<RegisterUserRequest>, RegisterUserRequestValidator>()
                .AddScoped<IValidator<UpdateUserRequest>, UpdateUserRequestValidator>()
                .AddScoped<IValidator<DeleteUserRequest>, DeleteUserRequestValidator>();

        private static IServiceCollection AddAuthenticationValidators(this IServiceCollection services) =>
            services.AddScoped<IValidator<AuthenticateUserRequest>, AuthenticateUserRequestValidator>();
    }
}
