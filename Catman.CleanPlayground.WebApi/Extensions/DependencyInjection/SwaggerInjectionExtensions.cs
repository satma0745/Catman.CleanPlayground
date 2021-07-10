namespace Catman.CleanPlayground.WebApi.Extensions.DependencyInjection
{
    using Catman.CleanPlayground.Application.Extensions.Configuration;
    using Catman.CleanPlayground.WebApi.Filters;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.OpenApi.Models;

    internal static class SwaggerInjectionExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services, IConfiguration configuration) =>
            services.AddSwaggerGen(options =>
            {
                var title = configuration.GetApplicationTitle();
                var version = $"v{configuration.GetApplicationVersion()}";
                
                options.SwaggerDoc(version, new OpenApiInfo
                {
                    Title = title,
                    Version = version
                });
                
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Provide authorization token:",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });

                options.OperationFilter<SwaggerAuthorizationIndicatorFilter>();
            });

        public static IApplicationBuilder UseSwagger(
            this IApplicationBuilder application,
            IConfiguration configuration) =>
            application
                .UseSwagger()
                .UseSwaggerUI(options =>
                {
                    var title = configuration.GetApplicationTitle();
                    var version = $"v{configuration.GetApplicationVersion()}";
                    
                    options.SwaggerEndpoint(
                        url: $"/swagger/{version}/swagger.json",
                        name: $"{title} {version}");

                    options.RoutePrefix = string.Empty;
                });
    }
}
