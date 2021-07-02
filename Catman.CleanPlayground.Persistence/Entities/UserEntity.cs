namespace Catman.CleanPlayground.Persistence.Entities
{
    internal class UserEntity
    {
        public byte Id { get; set; }
        
        public string Username { get; set; }
        
        public string Password { get; set; }
        
        public string DisplayName { get; set; }
    }
}
