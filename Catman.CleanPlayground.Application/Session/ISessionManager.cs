namespace Catman.CleanPlayground.Application.Session
{
    using System;
    using System.Threading.Tasks;

    public interface ISessionManager
    {
        public string GenerateAuthorizationToken(Guid userId);

        public Task<ISessionGenerationResult> RestoreSessionAsync(string authorizationToken);
    }
}
