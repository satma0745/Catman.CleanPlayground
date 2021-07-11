namespace Catman.CleanPlayground.Localization.Extensions.LocalizedString
{
    using Microsoft.Extensions.Localization;

    internal static class LocalizedStringSubstitutionExtensions
    {
        public static LocalizedString Inject(this LocalizedString localizedString, string key, string value) =>
            new LocalizedString(localizedString.Name, localizedString.Value.Replace($"{{{key}}}", value));

        public static LocalizedString InjectMinLength(this LocalizedString localizedString, int minLength) =>
            localizedString.Inject("minLen", minLength.ToString());

        public static LocalizedString InjectMaxLength(this LocalizedString localizedString, int maxLength) =>
            localizedString.Inject("maxLen", maxLength.ToString());
    }
}
