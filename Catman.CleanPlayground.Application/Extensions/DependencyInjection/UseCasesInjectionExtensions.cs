namespace Catman.CleanPlayground.Application.Extensions.DependencyInjection
{
    using System.Reflection;
    using Catman.CleanPlayground.Application.UseCases.Common.RequestHandling;
    using MediatR;
    using Microsoft.Extensions.DependencyInjection;

    internal static class UseCasesInjectionExtensions
    {
        public static IServiceCollection AddUseCases(this IServiceCollection services) =>
            services
                .AddMediatR(Assembly.GetExecutingAssembly())
                .AddScoped(typeof(IPipelineBehavior<,>), typeof(RequestValidationPipelineBehavior<,>))
                .AddScoped(typeof(IPipelineBehavior<,>), typeof(SessionRequestPipelineBehavior<,>));
    }
}
