namespace Catman.CleanPlayground.WebApi.Extensions.DependencyInjection
{
    using Catman.CleanPlayground.WebApi.DataTransferObjects.Authentication;
    using Catman.CleanPlayground.WebApi.DataTransferObjects.User;
    using FluentValidation;
    using Microsoft.Extensions.DependencyInjection;

    internal static class FluentValidationExtensions
    {
        public static IServiceCollection AddValidators(this IServiceCollection services) =>
            services
                .AddUserValidators()
                .AddAuthenticationValidators();
        
        private static IServiceCollection AddUserValidators(this IServiceCollection services) =>
            services
                .AddScoped<IValidator<RegisterUserDto>, RegisterUserDtoValidator>()
                .AddScoped<IValidator<UpdateUserDto>, UpdateUserDtoValidator>();

        private static IServiceCollection AddAuthenticationValidators(this IServiceCollection services) =>
            services.AddScoped<IValidator<UserCredentialsDto>, UserCredentialsDtoValidator>();
    }
}
