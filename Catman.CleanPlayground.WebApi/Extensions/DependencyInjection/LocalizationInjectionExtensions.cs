namespace Catman.CleanPlayground.WebApi.Extensions.DependencyInjection
{
    using System.Globalization;
    using Microsoft.AspNetCore.Builder;

    internal static class LocalizationInjectionExtensions
    {
        public static IApplicationBuilder UseLocalization(this IApplicationBuilder application) =>
            application.UseRequestLocalization(options =>
            {
                var supportedCultures = new[]
                {
                    new CultureInfo("en"),
                    new CultureInfo("ru")
                };

                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });
    }
}
