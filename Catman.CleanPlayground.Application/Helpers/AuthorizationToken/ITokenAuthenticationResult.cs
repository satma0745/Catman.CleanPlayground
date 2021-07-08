namespace Catman.CleanPlayground.Application.Helpers.AuthorizationToken
{
    using System;

    public interface ITokenAuthenticationResult
    {
        bool Success { get; }
        
        Guid UserId { get; }
        
        string ErrorMessage { get; }
    }
}
