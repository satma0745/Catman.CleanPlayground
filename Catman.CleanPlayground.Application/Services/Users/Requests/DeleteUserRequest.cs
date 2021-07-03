namespace Catman.CleanPlayground.Application.Services.Users.Requests
{
    using System;
    using FluentValidation;

    public class DeleteUserRequest
    {
        public Guid Id { get; set; }
        
        public string AuthenticationToken { get; set; }
    }

    internal class DeleteUserRequestValidator : AbstractValidator<DeleteUserRequest>
    {
        public DeleteUserRequestValidator()
        {
            RuleFor(request => request.Id).NotEmpty();
            RuleFor(request => request.AuthenticationToken).NotEmpty();
        }
    }
}
