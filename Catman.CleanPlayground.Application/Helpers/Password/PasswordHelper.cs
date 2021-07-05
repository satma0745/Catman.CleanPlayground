namespace Catman.CleanPlayground.Application.Helpers.Password
{
    using BCrypt.Net;

    internal class PasswordHelper : IPasswordHelper
    {
        public (string Hash, string Salt) HashPassword(string rawPassword) =>
            HashPasswordUsingSalt(rawPassword, BCrypt.GenerateSalt());

        public string HashPassword(string rawPassword, string salt) =>
            HashPasswordUsingSalt(rawPassword, salt).Hash;

        public bool IsSamePassword((string Hash, string Salt) password, string rawPassword) =>
            HashPassword(rawPassword, password.Salt) == password.Hash;
        
        private static (string Hash, string Salt) HashPasswordUsingSalt(string rawPassword, string salt) =>
            (BCrypt.HashPassword(rawPassword, salt), salt);
    }
}
