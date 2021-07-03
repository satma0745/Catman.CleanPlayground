namespace Catman.CleanPlayground.Application.Services.Users.Requests
{
    using System;
    using Catman.CleanPlayground.Application.Extensions.Validation;
    using FluentValidation;

    public class UpdateUserRequest
    {
        public Guid Id { get; set; }
        
        public string Username { get; set; }
        
        public string Password { get; set; }
        
        public string DisplayName { get; set; }
        
        public string AuthenticationToken { get; set; }
    }

    internal class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
    {
        public UpdateUserRequestValidator()
        {
            RuleFor(request => request.Id).NotEmpty();
            RuleFor(request => request.Username).ValidUsername();
            RuleFor(request => request.Password).ValidPassword();
            RuleFor(request => request.DisplayName).ValidDisplayName();
            RuleFor(request => request.AuthenticationToken).NotEmpty();
        }
    }
}
