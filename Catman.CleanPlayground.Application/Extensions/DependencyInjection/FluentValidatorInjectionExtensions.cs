namespace Catman.CleanPlayground.Application.Extensions.DependencyInjection
{
    using System.Reflection;
    using FluentValidation;
    using Microsoft.Extensions.DependencyInjection;

    internal static class FluentValidatorInjectionExtensions
    {
        public static IServiceCollection AddValidators(this IServiceCollection services) =>
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly(), includeInternalTypes: true);
    }
}
