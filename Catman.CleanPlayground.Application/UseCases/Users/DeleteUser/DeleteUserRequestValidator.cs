namespace Catman.CleanPlayground.Application.UseCases.Users.DeleteUser
{
    using Catman.CleanPlayground.Application.Extensions.Validation;
    using Catman.CleanPlayground.Application.Localization;
    using FluentValidation;

    internal class DeleteUserRequestValidator : AbstractValidator<DeleteUserRequest>
    {
        public DeleteUserRequestValidator(IValidationLocalizer localizer)
        {
            RuleFor(request => request.Id).Required(localizer);
        }
    }
}
