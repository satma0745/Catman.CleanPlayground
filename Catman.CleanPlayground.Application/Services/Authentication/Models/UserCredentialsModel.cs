namespace Catman.CleanPlayground.Application.Services.Authentication.Models
{
    using FluentValidation;

    public class UserCredentialsModel
    {
        public string Username { get; set; }
        
        public string Password { get; set; }
    }

    internal class UserCredentialsModelValidator : AbstractValidator<UserCredentialsModel>
    {
        public UserCredentialsModelValidator()
        {
            RuleFor(model => model.Username).NotEmpty();
            RuleFor(model => model.Password).NotEmpty();
        }
    }
}
