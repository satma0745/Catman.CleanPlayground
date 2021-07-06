namespace Catman.CleanPlayground.Application.Services.Users.RegisterUser
{
    using Catman.CleanPlayground.Application.Extensions.Validation;
    using FluentValidation;

    internal class RegisterUserRequestValidator : AbstractValidator<RegisterUserRequest>
    {
        public RegisterUserRequestValidator()
        {
            RuleFor(request => request.Username).ValidUsername();
            RuleFor(request => request.Password).ValidPassword();
            RuleFor(request => request.DisplayName).ValidDisplayName();
        }
    }
}