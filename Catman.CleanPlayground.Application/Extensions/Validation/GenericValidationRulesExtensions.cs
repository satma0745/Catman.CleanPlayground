namespace Catman.CleanPlayground.Application.Extensions.Validation
{
    using Catman.CleanPlayground.Application.Localization;
    using FluentValidation;

    public static class GenericValidationRulesExtensions
    {
        public static IRuleBuilderOptions<TModel, TProperty> Required<TModel, TProperty>(
            this IRuleBuilder<TModel, TProperty> property,
            IValidationLocalizer localizer) =>
            property
                .NotEmpty()
                .WithMessage(_ => localizer.Required());

        public static IRuleBuilderOptions<TModel, string> MinLength<TModel>(
            this IRuleBuilder<TModel, string> property,
            int minimumLength,
            IValidationLocalizer localizer) =>
            property
                .MinimumLength(minimumLength)
                .WithMessage(_ => localizer.MinLength(minimumLength));

        public static IRuleBuilderOptions<TModel, string> MaxLength<TModel>(
            this IRuleBuilder<TModel, string> property,
            int maximumLength,
            IValidationLocalizer localizer) =>
            property
                .MaximumLength(maximumLength)
                .WithMessage(_ => localizer.MaxLength(maximumLength));
    }
}
