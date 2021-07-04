namespace Catman.CleanPlayground.JwtAuthentication.Session
{
    using System;
    using Catman.CleanPlayground.Application.Session;

    internal class ApplicationUser : IApplicationUser
    {
        public Guid Id { get; set; }
        
        public string Username { get; set; }
        
        public string DisplayName { get; set; }
    }
}
