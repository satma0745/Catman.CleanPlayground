namespace Catman.CleanPlayground.Localization.Localizers.User
{
    using Catman.CleanPlayground.Application.Localization;
    using Microsoft.Extensions.Localization;

    internal class UserLocalizer : IUserLocalizer
    {
        private readonly IStringLocalizer<UserLocalizer> _localizer;

        public UserLocalizer(IStringLocalizer<UserLocalizer> localizer)
        {
            _localizer = localizer;
        }

        public string NotFound() =>
            _localizer["Not found"];

        public string IncorrectPassword() =>
            _localizer["Incorrect password"];

        public string UsernameAlreadyTaken() =>
            _localizer["Username taken"];

        public string AttemptToEditAnotherUser() =>
            _localizer["Attempt to edit another user"];

        public string AttemptToDeleteAnotherUser() =>
            _localizer["Attempt to delete another user"];
    }
}
