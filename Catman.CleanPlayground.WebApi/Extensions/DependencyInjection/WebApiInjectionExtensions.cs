namespace Catman.CleanPlayground.WebApi.Extensions.DependencyInjection
{
    using Microsoft.Extensions.DependencyInjection;

    internal static class WebApiInjectionExtensions
    {
        public static void AddWebApi(this IServiceCollection services) =>
            services
                .AddValidators()
                .AddControllers();
    }
}
