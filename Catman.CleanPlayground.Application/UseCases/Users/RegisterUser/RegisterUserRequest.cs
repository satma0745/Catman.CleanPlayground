namespace Catman.CleanPlayground.Application.UseCases.Users.RegisterUser
{
    using Catman.CleanPlayground.Application.UseCases.Common.Request;

    public class RegisterUserRequest : RequestBase
    {
        public string Username { get; set; }
        
        public string Password { get; set; }
        
        public string DisplayName { get; set; }
    }
}
