namespace Catman.CleanPlayground.Application.Services.Users.Models
{
    using System;

    public class UserModel
    {
        public Guid Id { get; set; }
        
        public string Username { get; set; }
        
        public string DisplayName { get; set; }
    }
}
