namespace Catman.CleanPlayground.Persistence.Helpers
{
    using BCrypt.Net;
    using Catman.CleanPlayground.Persistence.Entities;

    internal static class PasswordHelper
    {
        public static UserPassword HashPassword(string password) =>
            HashPassword(password, BCrypt.GenerateSalt());

        public static bool VerifyPassword(UserPassword hashedPassword, string possiblePassword)
        {
            var hashedPossiblePassword = HashPassword(possiblePassword, hashedPassword.Salt);
            return hashedPossiblePassword.Hash == hashedPassword.Hash;
        }
        
        private static UserPassword HashPassword(string password, string salt) =>
            new UserPassword
            {
                Salt = salt,
                Hash = BCrypt.HashPassword(password, salt)
            };
    }
}
