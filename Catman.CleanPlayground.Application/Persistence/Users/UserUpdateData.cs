namespace Catman.CleanPlayground.Application.Persistence.Users
{
    using System;

    public class UserUpdateData
    {
        public Guid Id { get; set; }
        
        public string Username { get; set; }
        
        public string Password { get; set; }
        
        public string DisplayName { get; set; }
    }
}
