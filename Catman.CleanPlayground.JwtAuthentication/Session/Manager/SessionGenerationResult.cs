namespace Catman.CleanPlayground.JwtAuthentication.Session.Manager
{
    using Catman.CleanPlayground.Application.Session;

    internal class SessionGenerationResult : ISessionGenerationResult
    {
        public bool Success { get; private init; }
        
        public ISession Session { get; private init; }
        
        public string ValidationError { get; private init; }

        public static SessionGenerationResult Authorized(ISession session) =>
            new SessionGenerationResult
            {
                Success = true,
                Session = session
            };

        public static SessionGenerationResult ValidationFailed(string validationErrorMessage) =>
            new SessionGenerationResult
            {
                Success = false,
                ValidationError = validationErrorMessage,
                Session = SessionInfo.Unauthorized()
            };
    }
}
