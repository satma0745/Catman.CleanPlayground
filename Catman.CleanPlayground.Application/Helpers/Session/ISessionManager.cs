namespace Catman.CleanPlayground.Application.Helpers.Session
{
    using System;
    using System.Threading.Tasks;

    public interface ISessionManager
    {
        bool Authorized { get; }
        
        IApplicationUser CurrentUser { get; }

        Task AuthorizeUserAsync(Guid userId);
    }
}
