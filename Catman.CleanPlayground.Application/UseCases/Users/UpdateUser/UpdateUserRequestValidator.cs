namespace Catman.CleanPlayground.Application.UseCases.Users.UpdateUser
{
    using Catman.CleanPlayground.Application.Extensions.Validation;
    using Catman.CleanPlayground.Application.Localization;
    using FluentValidation;

    internal class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
    {
        public UpdateUserRequestValidator(IValidationLocalizer localizer)
        {
            RuleFor(request => request.Id).Required(localizer);
            RuleFor(request => request.Username).ValidUsername(localizer);
            RuleFor(request => request.Password).ValidPassword(localizer);
            RuleFor(request => request.DisplayName).ValidDisplayName(localizer);
        }
    }
}
