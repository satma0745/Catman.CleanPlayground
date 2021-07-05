namespace Catman.CleanPlayground.Application.Helpers.Password
{
    using Catman.CleanPlayground.Application.Persistence.Entities;

    internal interface IPasswordHelper
    {
        UserEntity.UserPassword HashPassword(string rawPassword);

        string HashPassword(string rawPassword, string salt);

        bool IsSamePassword(UserEntity.UserPassword password, string rawPassword);
    }
}
