namespace Catman.CleanPlayground.Application.Services.Users.UpdateUser
{
    using Catman.CleanPlayground.Application.Extensions.Validation;
    using FluentValidation;

    internal class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
    {
        public UpdateUserRequestValidator()
        {
            RuleFor(request => request.Id).NotEmpty();
            RuleFor(request => request.Username).ValidUsername();
            RuleFor(request => request.Password).ValidPassword();
            RuleFor(request => request.DisplayName).ValidDisplayName();
        }
    }
}