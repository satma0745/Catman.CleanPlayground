namespace Catman.CleanPlayground.Services.Users
{
    internal class UpdateUserModel
    {
        public byte Id { get; set; }
        
        public string Username { get; set; }
        
        public string Password { get; set; }
        
        public string DisplayName { get; set; }
    }
}
