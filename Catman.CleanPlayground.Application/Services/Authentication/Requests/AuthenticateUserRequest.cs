namespace Catman.CleanPlayground.Application.Services.Authentication.Requests
{
    using Catman.CleanPlayground.Application.Services.Common.Request;
    using FluentValidation;

    public class AuthenticateUserRequest : RequestBase
    {
        public string Username { get; set; }
        
        public string Password { get; set; }
    }

    internal class AuthenticateUserRequestValidator : AbstractValidator<AuthenticateUserRequest>
    {
        public AuthenticateUserRequestValidator()
        {
            RuleFor(request => request.Username).NotEmpty();
            RuleFor(request => request.Password).NotEmpty();
        }
    }
}
