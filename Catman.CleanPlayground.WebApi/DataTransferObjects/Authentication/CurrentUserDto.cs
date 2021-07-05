namespace Catman.CleanPlayground.WebApi.DataTransferObjects.Authentication
{
    using System;

    public class CurrentUserDto
    {
        public Guid Id { get; set; }
        
        public string Username { get; set; }
        
        public string DisplayName { get; set; }
    }
}
