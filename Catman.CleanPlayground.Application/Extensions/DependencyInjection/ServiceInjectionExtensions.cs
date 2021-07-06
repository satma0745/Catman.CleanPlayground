namespace Catman.CleanPlayground.Application.Extensions.DependencyInjection
{
    using System.Linq;
    using System.Reflection;
    using Catman.CleanPlayground.Application.Extensions.Reflection;
    using Catman.CleanPlayground.Application.UseCases.Authentication;
    using Catman.CleanPlayground.Application.UseCases.Common.RequestHandler;
    using Catman.CleanPlayground.Application.UseCases.Common.RequestHandler.Handler;
    using Catman.CleanPlayground.Application.UseCases.Users;
    using Microsoft.Extensions.DependencyInjection;

    internal static class ServiceInjectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            var types = Assembly.GetExecutingAssembly().DefinedTypes;
            var requestHandlerTypes = types
                .Where(type => !type.IsInterface && !type.IsAbstract)
                .Where(type => type.Inherits(typeof(RequestHandlerBase<,>)));
            foreach (var concreteRequestHandlerType in requestHandlerTypes)
            {
                var abstractRequestHandlerType = concreteRequestHandlerType.BaseType!
                    .GetImplementedGenericInterface(typeof(IRequestHandler<,>));

                services.AddScoped(abstractRequestHandlerType, concreteRequestHandlerType);
            }

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            
            return services;
        }
    }
}
