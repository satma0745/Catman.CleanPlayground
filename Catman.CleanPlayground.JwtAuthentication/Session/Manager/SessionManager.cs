namespace Catman.CleanPlayground.JwtAuthentication.Session.Manager
{
    using System;
    using System.Threading.Tasks;
    using Catman.CleanPlayground.Application.Session;

    internal class SessionManager : ISessionManager
    {
        private readonly SessionTokenGenerator _tokenGenerator;
        private readonly SessionGenerator _sessionGenerator;

        public SessionManager(SessionTokenGenerator tokenGenerator, SessionGenerator sessionGenerator)
        {
            _tokenGenerator = tokenGenerator;
            _sessionGenerator = sessionGenerator;
        }

        public string GenerateAuthorizationToken(Guid userId) =>
            _tokenGenerator.GenerateToken(userId);

        public Task<ISessionGenerationResult> RestoreSessionAsync(string authorizationToken) =>
            _sessionGenerator.GenerateSessionAsync(authorizationToken);
    }
}
