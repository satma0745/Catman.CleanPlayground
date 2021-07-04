namespace Catman.CleanPlayground.Application.Services.Common.Request
{
    using System;

    public class ApplicationUser
    {
        public Guid Id { get; init; }
        
        public string Username { get; init; }
        
        public string DisplayName { get; init; }
    }
}
