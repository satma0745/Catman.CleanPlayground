namespace Catman.CleanPlayground.Application.Services.Users.Models
{
    using System;
    using FluentValidation;

    public class DeleteUserModel
    {
        public Guid Id { get; set; }
        
        public string AuthenticationToken { get; set; }
    }

    internal class DeleteUserModelValidator : AbstractValidator<DeleteUserModel>
    {
        public DeleteUserModelValidator()
        {
            RuleFor(model => model.Id).NotEmpty();
            RuleFor(model => model.AuthenticationToken).NotEmpty();
        }
    }
}
