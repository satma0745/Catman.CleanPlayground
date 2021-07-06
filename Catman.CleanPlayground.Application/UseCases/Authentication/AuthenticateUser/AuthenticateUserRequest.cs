namespace Catman.CleanPlayground.Application.UseCases.Authentication.AuthenticateUser
{
    using Catman.CleanPlayground.Application.UseCases.Common.Request;

    public class AuthenticateUserRequest : RequestBase
    {
        public string Username { get; set; }
        
        public string Password { get; set; }
    }
}
