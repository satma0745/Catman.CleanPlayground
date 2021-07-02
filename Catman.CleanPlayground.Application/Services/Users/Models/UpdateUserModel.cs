namespace Catman.CleanPlayground.Application.Services.Users.Models
{
    using System;

    public class UpdateUserModel
    {
        public Guid Id { get; set; }
        
        public string Username { get; set; }
        
        public string Password { get; set; }
        
        public string DisplayName { get; set; }
    }
}
