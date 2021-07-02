namespace Catman.CleanPlayground.Application.Extensions.DependencyInjection
{
    using Catman.CleanPlayground.Application.Services.Users.Models;
    using FluentValidation;
    using Microsoft.Extensions.DependencyInjection;

    internal static class FluentValidatorDependencyInjection
    {
        public static IServiceCollection AddValidators(this IServiceCollection services) =>
            services
                .AddScoped<IValidator<RegisterUserModel>, RegisterUserModelValidator>()
                .AddScoped<IValidator<UpdateUserModel>, UpdateUserModelValidator>();
    }
}
