namespace Catman.CleanPlayground.Application.Persistence.Entities
{
    using System;

    public class UserEntity
    {
        public Guid Id { get; set; }
        
        public string Username { get; set; }
        
        public UserPassword Password { get; set; } 
        
        public string DisplayName { get; set; }
        
        public class UserPassword
        {
            public string Hash { get; set; }

            public string Salt { get; set; }
        }
    }
}
