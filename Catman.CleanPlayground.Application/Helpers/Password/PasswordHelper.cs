namespace Catman.CleanPlayground.Application.Helpers.Password
{
    using BCrypt.Net;
    using Catman.CleanPlayground.Application.Persistence.Entities;

    internal class PasswordHelper : IPasswordHelper
    {
        public UserEntity.UserPassword HashPassword(string rawPassword) =>
            HashPasswordUsingSalt(rawPassword, BCrypt.GenerateSalt());

        public string HashPassword(string rawPassword, string salt) =>
            HashPasswordUsingSalt(rawPassword, salt).Hash;

        public bool IsSamePassword(UserEntity.UserPassword password, string rawPassword) =>
            HashPassword(rawPassword, password.Salt) == password.Hash;
        
        private static UserEntity.UserPassword HashPasswordUsingSalt(string rawPassword, string salt) =>
            new UserEntity.UserPassword
            {
                Hash = BCrypt.HashPassword(rawPassword, salt),
                Salt = salt
            };
    }
}
