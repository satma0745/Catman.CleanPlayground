namespace Catman.CleanPlayground.Application.UseCases.Users.GetUsers
{
    using System;

    public class UserResource
    {
        public Guid Id { get; set; }
        
        public string Username { get; set; }
        
        public string DisplayName { get; set; }
    }
}
