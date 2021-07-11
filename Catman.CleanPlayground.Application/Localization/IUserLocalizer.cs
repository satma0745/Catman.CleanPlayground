namespace Catman.CleanPlayground.Application.Localization
{
    public interface IUserLocalizer
    {
        string NotFound();

        string IncorrectPassword();

        string UsernameAlreadyTaken();

        string AttemptToEditAnotherUser();

        string AttemptToDeleteAnotherUser();
    }
}
