namespace Catman.CleanPlayground.Application.Services.Users.Models
{
    using Catman.CleanPlayground.Application.Extensions.Validation;
    using FluentValidation;

    public class RegisterUserModel
    {
        public string Username { get; set; }
        
        public string Password { get; set; }
        
        public string DisplayName { get; set; }
    }

    internal class RegisterUserModelValidator : AbstractValidator<RegisterUserModel>
    {
        public RegisterUserModelValidator()
        {
            RuleFor(model => model.Username).ValidUsername();
            RuleFor(model => model.Password).ValidPassword();
            RuleFor(model => model.DisplayName).ValidDisplayName();
        }
    }
}
