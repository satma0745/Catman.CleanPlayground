namespace Catman.CleanPlayground.Application.Extensions.DependencyInjection
{
    using System.Reflection;
    using MediatR;
    using Microsoft.Extensions.DependencyInjection;

    internal static class UseCasesInjectionExtensions
    {
        public static IServiceCollection AddUseCases(this IServiceCollection services) =>
            services.AddMediatR(Assembly.GetExecutingAssembly());
    }
}
