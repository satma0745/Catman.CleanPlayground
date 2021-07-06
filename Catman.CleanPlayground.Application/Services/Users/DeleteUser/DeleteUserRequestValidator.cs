namespace Catman.CleanPlayground.Application.Services.Users.DeleteUser
{
    using FluentValidation;

    internal class DeleteUserRequestValidator : AbstractValidator<DeleteUserRequest>
    {
        public DeleteUserRequestValidator()
        {
            RuleFor(request => request.Id).NotEmpty();
        }
    }
}