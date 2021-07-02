namespace Catman.CleanPlayground.WebApi.Extensions.DependencyInjection
{
    using Catman.CleanPlayground.WebApi.DataObjects.User;
    using FluentValidation;
    using Microsoft.Extensions.DependencyInjection;

    internal static class FluentValidationExtensions
    {
        public static IServiceCollection AddValidators(this IServiceCollection services) =>
            services
                .AddScoped<IValidator<RegisterUserDto>, RegisterUserDtoValidator>()
                .AddScoped<IValidator<UpdateUserDto>, UpdateUserDtoValidator>();
    }
}
