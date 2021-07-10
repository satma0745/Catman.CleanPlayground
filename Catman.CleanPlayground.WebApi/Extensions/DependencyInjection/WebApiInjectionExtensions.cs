namespace Catman.CleanPlayground.WebApi.Extensions.DependencyInjection
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    internal static class WebApiInjectionExtensions
    {
        public static void AddWebApi(this IServiceCollection services, IConfiguration configuration) =>
            services
                .AddAuthentication(configuration)
                .AddSwagger(configuration)
                .AddControllers()
                .AddValidation();
    }
}
