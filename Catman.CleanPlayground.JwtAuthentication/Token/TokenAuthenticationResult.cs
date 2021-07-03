namespace Catman.CleanPlayground.JwtAuthentication.Token
{
    using System;
    using Catman.CleanPlayground.Application.Authentication;

    internal class TokenAuthenticationResult : ITokenAuthenticationResult
    {
        public bool IsValid { get; private init; }
        
        public Guid? UserId { get; private init; }
        
        public string ErrorMessage { get; private init; }

        private TokenAuthenticationResult()
        {
        }

        public static TokenAuthenticationResult ValidationPassed(Guid ownerId) =>
            new TokenAuthenticationResult
            {
                IsValid = true,
                UserId = ownerId
            };

        public static TokenAuthenticationResult ValidationFailed(string message) =>
            new TokenAuthenticationResult
            {
                IsValid = false,
                ErrorMessage = message
            };
    }
}
