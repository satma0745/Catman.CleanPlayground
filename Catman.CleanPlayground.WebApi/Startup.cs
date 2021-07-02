namespace Catman.CleanPlayground.WebApi
{
    using Catman.CleanPlayground.Application.Extensions.DependencyInjection;
    using Catman.CleanPlayground.Persistence.Extensions.DependencyInjection;
    using Catman.CleanPlayground.WebApi.Extensions.DependencyInjection;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.DependencyInjection;
    
    internal class Startup
    {
        public static void ConfigureServices(IServiceCollection services) =>
            services
                .AddApplication()
                .AddPersistence()
                .AddMappings()
                .AddWebApi();

        public static void Configure(IApplicationBuilder application, IWebHostEnvironment environment) =>
            application
                .UseRouting()
                .UseEndpoints(endpoints => endpoints.MapControllers());
    }
}
