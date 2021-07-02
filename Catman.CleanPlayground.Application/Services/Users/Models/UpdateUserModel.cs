namespace Catman.CleanPlayground.Application.Services.Users.Models
{
    public class UpdateUserModel
    {
        public byte Id { get; set; }
        
        public string Username { get; set; }
        
        public string Password { get; set; }
        
        public string DisplayName { get; set; }
    }
}
