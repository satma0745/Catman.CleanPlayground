namespace Catman.CleanPlayground.Application.Services.Authentication.AuthenticateUser
{
    using Catman.CleanPlayground.Application.Services.Common.Request;

    public class AuthenticateUserRequest : RequestBase
    {
        public string Username { get; set; }
        
        public string Password { get; set; }
    }
}
