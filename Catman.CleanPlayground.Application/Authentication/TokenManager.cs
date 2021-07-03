namespace Catman.CleanPlayground.Application.Authentication
{
    using System;

    internal class TokenManager
    {
        public string GenerateToken(Guid userId) =>
            $"Authentication token for user \"{userId}\"";
    }
}
