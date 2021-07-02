namespace Catman.CleanPlayground.Application.Services.Users.Models
{
    using System;
    using FluentValidation;

    public class UpdateUserModel
    {
        public Guid Id { get; set; }
        
        public string Username { get; set; }
        
        public string Password { get; set; }
        
        public string DisplayName { get; set; }
    }

    internal class UpdateUserModelValidator : AbstractValidator<UpdateUserModel>
    {
        public UpdateUserModelValidator()
        {
            RuleFor(model => model.Id).NotEmpty();
            
            RuleFor(model => model.Username)
                .MinimumLength(4)
                .MaximumLength(20)
                .NotNull();
            
            RuleFor(model => model.Password)
                .MinimumLength(4)
                .MaximumLength(20)
                .NotNull();
            
            RuleFor(model => model.DisplayName)
                .MinimumLength(4)
                .MaximumLength(40)
                .NotNull();
        }
    }
}
