namespace Catman.CleanPlayground.JwtAuthentication.TokenHelper
{
    using System;
    using Catman.CleanPlayground.Application.Helpers.AuthorizationToken;

    internal class TokenAuthenticationResult : ITokenAuthenticationResult
    {
        public bool Success { get; }
        
        public Guid UserId { get; }
        
        public string ErrorMessage { get; }

        public TokenAuthenticationResult(Guid userId)
        {
            Success = true;
            UserId = userId;
        }

        public TokenAuthenticationResult(string errorMessage)
        {
            Success = false;
            ErrorMessage = errorMessage;
        }
    }
}
