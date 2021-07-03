namespace Catman.CleanPlayground.Application.Authentication
{
    using System;

    public interface ITokenAuthenticationResult
    {
        bool IsValid { get; }
        
        Guid? UserId { get; }
        
        string ErrorMessage { get; }
    }
}
