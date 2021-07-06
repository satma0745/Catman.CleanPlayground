namespace Catman.CleanPlayground.Application.Extensions.DependencyInjection
{
    using System.Linq;
    using System.Reflection;
    using Catman.CleanPlayground.Application.Extensions.Reflection;
    using Catman.CleanPlayground.Application.UseCases.Common.RequestBroker;
    using Catman.CleanPlayground.Application.UseCases.Common.RequestHandler;
    using Microsoft.Extensions.DependencyInjection;

    internal static class UseCasesInjectionExtensions
    {
        public static IServiceCollection AddUseCases(this IServiceCollection services) =>
            services
                .AddRequestBroker()
                .AddRequestHandlers();

        private static IServiceCollection AddRequestBroker(this IServiceCollection services) =>
            services
                .AddScoped<IRequestBroker, RequestBroker>()
                .AddScoped(serviceProvider => serviceProvider);
        
        private static IServiceCollection AddRequestHandlers(this IServiceCollection services)
        {
            var requestHandlerTypes = Assembly.GetExecutingAssembly().DefinedTypes
                .Where(type => !type.IsInterface && !type.IsAbstract)
                .Where(type => type.ImplementsGenericInterface(typeof(IRequestHandler<,>)));
            
            foreach (var concreteRequestHandlerType in requestHandlerTypes)
            {
                var abstractRequestHandlerType = concreteRequestHandlerType.BaseType!
                    .GetImplementedGenericInterface(typeof(IRequestHandler<,>));

                services.AddScoped(abstractRequestHandlerType, concreteRequestHandlerType);
            }
            
            return services;
        }
    }
}
