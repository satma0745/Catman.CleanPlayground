namespace Catman.CleanPlayground.Localization.Extensions.DependencyInjection
{
    using Catman.CleanPlayground.Application.Localization;
    using Catman.CleanPlayground.Localization.Localizers.User;
    using Catman.CleanPlayground.Localization.Localizers.Validation;
    using Microsoft.Extensions.DependencyInjection;

    public static class LocalizationInjectionExtensions
    {
        public static IServiceCollection AddResourceLocalization(this IServiceCollection services) =>
            services
                .AddLocalization()
                .AddLocalizers();

        private static IServiceCollection AddLocalizers(this IServiceCollection services) =>
            services
                .AddScoped<IValidationLocalizer, ValidationLocalizer>()
                .AddScoped<IUserLocalizer, UserLocalizer>();
    }
}
