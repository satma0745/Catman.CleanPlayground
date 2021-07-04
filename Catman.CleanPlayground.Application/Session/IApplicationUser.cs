namespace Catman.CleanPlayground.Application.Session
{
    using System;

    public interface IApplicationUser
    {
        Guid Id { get; }
        
        string Username { get; }
        
        string DisplayName { get; }
    }
}
