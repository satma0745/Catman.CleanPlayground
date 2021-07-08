namespace Catman.CleanPlayground.Application.Helpers.Session
{
    using System;

    internal class ApplicationUser : IApplicationUser
    {
        public Guid Id { get; set; }
        
        public string Username { get; set; }
        
        public string DisplayName { get; set; }
    }
}
