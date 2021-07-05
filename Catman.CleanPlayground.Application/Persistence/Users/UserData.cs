namespace Catman.CleanPlayground.Application.Persistence.Users
{
    using System;

    public class UserData
    {
        public Guid Id { get; set; }
        
        public string Username { get; set; }
        
        public string PasswordHash { get; set; }
        
        public string PasswordSalt { get; set; }
        
        public string DisplayName { get; set; }
    }
}
