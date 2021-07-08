namespace Catman.CleanPlayground.WebApi
{
    using Catman.CleanPlayground.Application.Extensions.DependencyInjection;
    using Catman.CleanPlayground.JwtAuthentication.Extensions.DependencyInjection;
    using Catman.CleanPlayground.PostgreSqlPersistence.Extensions.DependencyInjection;
    using Catman.CleanPlayground.WebApi.Extensions.DependencyInjection;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    
    internal class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public void ConfigureServices(IServiceCollection services) =>
            services
                .AddApplication()
                .AddPostgreSqlPersistence(_configuration)
                .AddMappings()
                .AddJwtAuthentication()
                .AddWebApi(_configuration);

        public void Configure(IApplicationBuilder application, IWebHostEnvironment environment) =>
            application
                .UseSwagger(_configuration)
                .UseRouting()
                .UseEndpoints(endpoints => endpoints.MapControllers());
    }
}
