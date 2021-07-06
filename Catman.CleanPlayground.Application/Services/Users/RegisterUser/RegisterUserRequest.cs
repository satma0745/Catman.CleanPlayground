namespace Catman.CleanPlayground.Application.Services.Users.RegisterUser
{
    using Catman.CleanPlayground.Application.Services.Common.Request;

    public class RegisterUserRequest : RequestBase
    {
        public string Username { get; set; }
        
        public string Password { get; set; }
        
        public string DisplayName { get; set; }
    }
}
