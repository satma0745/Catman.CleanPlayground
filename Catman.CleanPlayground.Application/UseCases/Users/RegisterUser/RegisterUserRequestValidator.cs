namespace Catman.CleanPlayground.Application.UseCases.Users.RegisterUser
{
    using Catman.CleanPlayground.Application.Extensions.Validation;
    using Catman.CleanPlayground.Application.Localization;
    using FluentValidation;

    internal class RegisterUserRequestValidator : AbstractValidator<RegisterUserRequest>
    {
        public RegisterUserRequestValidator(IValidationLocalizer localizer)
        {
            RuleFor(request => request.Username).ValidUsername(localizer);
            RuleFor(request => request.Password).ValidPassword(localizer);
            RuleFor(request => request.DisplayName).ValidDisplayName(localizer);
        }
    }
}
