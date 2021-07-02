namespace Catman.CleanPlayground.Application.Persistence.Users
{
    using System;

    public class UsernameAvailabilityCheckParameters
    {
        public string Username { get; }
        
        public Guid? ExceptUserWithId { get; }

        public UsernameAvailabilityCheckParameters(string username)
        {
            Username = username;
        }

        public UsernameAvailabilityCheckParameters(string username, Guid exceptUserWithId)
            : this(username)
        {
            ExceptUserWithId = exceptUserWithId;
        }
    }
}
