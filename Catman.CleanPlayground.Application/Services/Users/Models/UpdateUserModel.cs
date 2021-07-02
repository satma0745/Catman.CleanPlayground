namespace Catman.CleanPlayground.Application.Services.Users.Models
{
    using System;
    using Catman.CleanPlayground.Application.Extensions.Validation;
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
            RuleFor(model => model.Username).ValidUsername();
            RuleFor(model => model.Password).ValidPassword();
            RuleFor(model => model.DisplayName).ValidDisplayName();
        }
    }
}
