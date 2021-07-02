namespace Catman.CleanPlayground.Application.Data.Users
{
    public class UsernameAvailabilityCheckParameters
    {
        public string Username { get; }
        
        public byte? ExceptUserWithId { get; }

        public UsernameAvailabilityCheckParameters(string username)
        {
            Username = username;
        }

        public UsernameAvailabilityCheckParameters(string username, byte exceptUserWithId)
            : this(username)
        {
            ExceptUserWithId = exceptUserWithId;
        }
    }
}
