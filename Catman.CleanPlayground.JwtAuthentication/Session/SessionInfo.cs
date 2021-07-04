namespace Catman.CleanPlayground.JwtAuthentication.Session
{
    using Catman.CleanPlayground.Application.Session;

    internal class SessionInfo : ISession
    {
        public bool Authorized { get; }
        
        public IApplicationUser CurrentUser { get; }

        private SessionInfo()
        {
        }

        public SessionInfo(IApplicationUser currentUser)
        {
            Authorized = true;
            CurrentUser = currentUser;
        }

        public static SessionInfo Unauthorized() =>
            new SessionInfo();
    }
}
