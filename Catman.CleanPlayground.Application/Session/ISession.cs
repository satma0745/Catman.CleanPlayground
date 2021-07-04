namespace Catman.CleanPlayground.Application.Session
{
    public interface ISession
    {
        bool Authorized { get; }
        
        IApplicationUser CurrentUser { get; }
    }
}
