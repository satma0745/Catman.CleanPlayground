namespace Catman.CleanPlayground.Application.Extensions.DependencyInjection
{
    using Catman.CleanPlayground.Application.Services.Authentication.Models;
    using Catman.CleanPlayground.Application.Services.Users.Models;
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
                .AddScoped<IValidator<RegisterUserModel>, RegisterUserModelValidator>()
                .AddScoped<IValidator<UpdateUserModel>, UpdateUserModelValidator>()
                .AddScoped<IValidator<DeleteUserModel>, DeleteUserModelValidator>();

        private static IServiceCollection AddAuthenticationValidators(this IServiceCollection services) =>
            services.AddScoped<IValidator<UserCredentialsModel>, UserCredentialsModelValidator>();
    }
}
