namespace Catman.CleanPlayground.Localization.Localizers.Validation
{
    using Catman.CleanPlayground.Application.Localization;
    using Catman.CleanPlayground.Localization.Extensions.LocalizedString;
    using Microsoft.Extensions.Localization;

    internal class ValidationLocalizer : IValidationLocalizer
    {
        private readonly IStringLocalizer<ValidationLocalizer> _localizer;

        public ValidationLocalizer(IStringLocalizer<ValidationLocalizer> localizer)
        {
            _localizer = localizer;
        }

        public string Required() =>
            _localizer["Required"];

        public string MinLength(int minLength) =>
            _localizer["Min length"].InjectMinLength(minLength);

        public string MaxLength(int maxLength) =>
            _localizer["Max length"].InjectMaxLength(maxLength);
    }
}
