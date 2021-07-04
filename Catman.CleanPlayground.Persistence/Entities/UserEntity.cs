namespace Catman.CleanPlayground.Persistence.Entities
{
    using System;

    internal class UserEntity
    {
        public Guid Id { get; set; }
        
        public string Username { get; set; }
        
        public UserPassword Password { get; set; }
        
        public string DisplayName { get; set; }
    }
}
