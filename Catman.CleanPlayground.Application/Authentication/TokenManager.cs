namespace Catman.CleanPlayground.Application.Authentication
{
    using System;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using Catman.CleanPlayground.Application.Persistence.Users;

    internal class TokenManager
    {
        private readonly IUserRepository _userRepository;

        public TokenManager(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        
        public string GenerateToken(Guid userId) =>
            $"Authentication token for user \"{userId}\"";

        public async Task<TokenAuthenticationResult> AuthenticateTokenAsync(string token)
        {
            var tokenRegex = new Regex(@"^Authentication token for user ""(?<OwnerId>.*)""$");
            
            var isFake = !tokenRegex.IsMatch(token);
            if (isFake)
            {
                return TokenAuthenticationResult.ValidationFailed("Fake authentication token provided.");
            }

            var ownerIdGroup = tokenRegex.Match(token).Groups["OwnerId"];
            if (!Guid.TryParse(ownerIdGroup.Value, out var tokenOwnerId))
            {
                return TokenAuthenticationResult.ValidationFailed("Fake authentication token provided.");
            }

            var ownerExists = await _userRepository.UserExistsAsync(tokenOwnerId);
            return ownerExists
                ? TokenAuthenticationResult.ValidationPassed(tokenOwnerId)
                : TokenAuthenticationResult.ValidationFailed("Fake authentication token provided.");
        }
    }
}
