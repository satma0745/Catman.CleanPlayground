namespace Catman.CleanPlayground.Application.Services.Authentication.AuthenticateUser
{
    using FluentValidation;

    internal class AuthenticateUserRequestValidator : AbstractValidator<AuthenticateUserRequest>
    {
        public AuthenticateUserRequestValidator()
        {
            RuleFor(request => request.Username).NotEmpty();
            RuleFor(request => request.Password).NotEmpty();
        }
    }
}
