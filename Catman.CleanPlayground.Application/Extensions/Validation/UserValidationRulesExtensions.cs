namespace Catman.CleanPlayground.Application.Extensions.Validation
{
    using Catman.CleanPlayground.Application.Localization;
    using FluentValidation;

    public static class UserValidationRulesExtensions
    {
        public static IRuleBuilderOptions<TModel, string> ValidUsername<TModel>(
            this IRuleBuilder<TModel, string> property,
            IValidationLocalizer localizer) =>
            property
                .MinLength(4, localizer)
                .MaxLength(20, localizer)
                .Required(localizer);
        
        public static IRuleBuilderOptions<TModel, string> ValidPassword<TModel>(
            this IRuleBuilder<TModel, string> property,
            IValidationLocalizer localizer) =>
            property
                .MinLength(4, localizer)
                .MaxLength(20, localizer)
                .Required(localizer);
        
        public static IRuleBuilderOptions<TModel, string> ValidDisplayName<TModel>(
            this IRuleBuilder<TModel, string> property,
            IValidationLocalizer localizer) =>
            property
                .MinLength(4, localizer)
                .MaxLength(40, localizer)
                .Required(localizer);
    }
}
