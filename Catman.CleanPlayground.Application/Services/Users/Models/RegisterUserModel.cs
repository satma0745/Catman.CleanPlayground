namespace Catman.CleanPlayground.Application.Services.Users.Models
{
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
