namespace Catman.CleanPlayground.JwtAuthentication.TokenHelper
{
    using System;

    internal class TokenPayload
    {
        public Guid UserId { get; init; }
        
        public byte Version { get; init; }
    }
}
