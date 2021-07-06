namespace Catman.CleanPlayground.Application.Services.Authentication.GetCurrentUser
{
    using System;

    public class CurrentUserResource
    {
        public Guid Id { get; set; }
        
        public string Username { get; set; }
        
        public string DisplayName { get; set; }
    }
}
