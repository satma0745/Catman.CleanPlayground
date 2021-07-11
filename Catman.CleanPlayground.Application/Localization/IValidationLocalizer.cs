namespace Catman.CleanPlayground.Application.Localization
{
    public interface IValidationLocalizer
    {
        string Required();

        string MinLength(int minLength);

        string MaxLength(int maxLength);
    }
}
