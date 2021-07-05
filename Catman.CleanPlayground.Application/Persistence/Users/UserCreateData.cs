namespace Catman.CleanPlayground.Application.Persistence.Users
{
    public class UserCreateData
    {
        public string Username { get; set; }
        
        public string PasswordHash { get; set; }
        
        public string PasswordSalt { get; set; }
        
        public string DisplayName { get; set; }
    }
}
