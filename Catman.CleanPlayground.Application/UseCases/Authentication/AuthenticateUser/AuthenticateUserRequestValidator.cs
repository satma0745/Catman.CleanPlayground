namespace Catman.CleanPlayground.Application.UseCases.Authentication.AuthenticateUser
{
    using Catman.CleanPlayground.Application.Extensions.Validation;
    using Catman.CleanPlayground.Application.Localization;
    using FluentValidation;

    internal class AuthenticateUserRequestValidator : AbstractValidator<AuthenticateUserRequest>
    {
        public AuthenticateUserRequestValidator(IValidationLocalizer localizer)
        {
            RuleFor(request => request.Username).Required(localizer);
            RuleFor(request => request.Password).Required(localizer);
        }
    }
}
