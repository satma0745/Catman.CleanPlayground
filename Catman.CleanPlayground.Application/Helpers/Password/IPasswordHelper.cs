namespace Catman.CleanPlayground.Application.Helpers.Password
{
    internal interface IPasswordHelper
    {
        (string Hash, string Salt) HashPassword(string rawPassword);

        string HashPassword(string rawPassword, string salt);

        bool IsSamePassword((string Hash, string Salt) password, string rawPassword);
    }
}
