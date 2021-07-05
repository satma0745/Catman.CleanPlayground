namespace Catman.CleanPlayground.WebApi.Extensions.DependencyInjection
{
    using FluentValidation.AspNetCore;
    using Microsoft.Extensions.DependencyInjection;

    internal static class FluentValidationExtensions
    {
        public static IMvcBuilder AddValidation(this IMvcBuilder mvcBuilder) =>
            mvcBuilder
                .AddFluentValidation(options =>
                {
                    options.RegisterValidatorsFromAssemblyContaining<Startup>(includeInternalTypes: true);
                    options.DisableDataAnnotationsValidation = true;
                });
    }
}
