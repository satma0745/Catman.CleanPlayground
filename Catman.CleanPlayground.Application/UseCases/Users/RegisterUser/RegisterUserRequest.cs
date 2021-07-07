namespace Catman.CleanPlayground.Application.UseCases.Users.RegisterUser
{
    using Catman.CleanPlayground.Application.UseCases.Common.Request;
    using Catman.CleanPlayground.Application.UseCases.Common.Response;

    public class RegisterUserRequest : RequestBase<BlankResource>
    {
        public string Username { get; set; }
        
        public string Password { get; set; }
        
        public string DisplayName { get; set; }
    }
}
