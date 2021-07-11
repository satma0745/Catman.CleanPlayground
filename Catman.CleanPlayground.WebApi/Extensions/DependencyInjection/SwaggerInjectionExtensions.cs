namespace Catman.CleanPlayground.WebApi.Extensions.DependencyInjection
{
    using Catman.CleanPlayground.Application.Extensions.Configuration;
    using Catman.CleanPlayground.WebApi.Filters;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.OpenApi.Models;
    using Swashbuckle.AspNetCore.SwaggerGen;
    using Swashbuckle.AspNetCore.SwaggerUI;

    internal static class SwaggerInjectionExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services, IConfiguration configuration) =>
            services.AddSwaggerGen(options =>
            {
                options.AddOpenApiInfo(configuration);
                options.AddSecurity();
            });

        public static IApplicationBuilder UseSwagger(
            this IApplicationBuilder application,
            IConfiguration configuration) =>
            application
                .UseSwagger()
                .UseSwaggerUI(options =>
                {
                    options.AddSwaggerEndpoint(configuration);
                    options.InjectStylesheet("/swagger.css");
                    options.DisableSchemasSection();
                });

        private static void AddOpenApiInfo(this SwaggerGenOptions options, IConfiguration configuration)
        {
            var title = configuration.GetApplicationTitle();
            var version = $"v{configuration.GetApplicationVersion()}";
                
            options.SwaggerDoc(version, new OpenApiInfo
            {
                Title = title,
                Version = version
            });
        }

        private static void AddSecurity(this SwaggerGenOptions options)
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "Provide authorization token:",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "bearer"
            });

            options.OperationFilter<SwaggerAuthorizationIndicatorFilter>();
        }

        private static void AddSwaggerEndpoint(this SwaggerUIOptions options, IConfiguration configuration)
        {
            var title = configuration.GetApplicationTitle();
            var version = $"v{configuration.GetApplicationVersion()}";
                    
            options.SwaggerEndpoint(
                url: $"/swagger/{version}/swagger.json",
                name: $"{title} {version}");
            
            options.RoutePrefix = string.Empty;
        }

        private static void DisableSchemasSection(this SwaggerUIOptions options) =>
            options.DefaultModelsExpandDepth(-1);
    }
}
