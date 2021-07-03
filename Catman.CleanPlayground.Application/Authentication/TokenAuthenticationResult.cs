namespace Catman.CleanPlayground.Application.Authentication
{
    using System;

    internal class TokenAuthenticationResult
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

        public static TokenAuthenticationResult ValidationFailed(string error) =>
            new TokenAuthenticationResult
            {
                IsValid = false,
                ErrorMessage = error
            };
    }
}
