namespace Catman.CleanPlayground.Application.Extensions.Validation
{
    using FluentValidation;

    public static class UserValidationExtensions
    {
        public static IRuleBuilderOptions<TModel, string> ValidUsername<TModel>(
            this IRuleBuilder<TModel, string> property) =>
            property
                .MinimumLength(4)
                .MaximumLength(20)
                .NotNull();
        
        public static IRuleBuilderOptions<TModel, string> ValidPassword<TModel>(
            this IRuleBuilder<TModel, string> property) =>
            property
                .MinimumLength(4)
                .MaximumLength(20)
                .NotNull();
        
        public static IRuleBuilderOptions<TModel, string> ValidDisplayName<TModel>(
            this IRuleBuilder<TModel, string> property) =>
            property
                .MinimumLength(4)
                .MaximumLength(40)
                .NotNull();
    }
}
