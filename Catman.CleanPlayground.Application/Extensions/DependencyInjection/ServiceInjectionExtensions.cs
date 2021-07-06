namespace Catman.CleanPlayground.Application.Extensions.DependencyInjection
{
    using System.Linq;
    using System.Reflection;
    using Catman.CleanPlayground.Application.Extensions.Reflection;
    using Catman.CleanPlayground.Application.Services.Authentication;
    using Catman.CleanPlayground.Application.Services.Common.Operation;
    using Catman.CleanPlayground.Application.Services.Common.Operation.Handler;
    using Catman.CleanPlayground.Application.Services.Users;
    using Microsoft.Extensions.DependencyInjection;

    internal static class ServiceInjectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            var types = Assembly.GetExecutingAssembly().DefinedTypes;
            var operationHandlerTypes = types
                .Where(type => !type.IsInterface && !type.IsAbstract)
                .Where(type => type.Inherits(typeof(OperationHandlerBase<,>)));
            foreach (var operationHandlerType in operationHandlerTypes)
            {
                var operationType = operationHandlerType.BaseType!
                    .GetImplementedGenericInterface(typeof(IOperation<,>));

                services.AddScoped(operationType, operationHandlerType);
            }

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            
            return services;
        }
    }
}
