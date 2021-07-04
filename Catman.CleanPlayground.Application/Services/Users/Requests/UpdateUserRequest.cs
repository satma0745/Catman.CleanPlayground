namespace Catman.CleanPlayground.Application.Services.Users.Requests
{
    using System;
    using Catman.CleanPlayground.Application.Extensions.Validation;
    using Catman.CleanPlayground.Application.Services.Common.Request;
    using FluentValidation;

    public class UpdateUserRequest : RequestBase
    {
        public Guid Id { get; }
        
        public string Username { get; set; }
        
        public string Password { get; set; }
        
        public string DisplayName { get; set; }

        public UpdateUserRequest(Guid id, string authorizationToken)
            : base(authorizationToken)
        {
            Id = id;
        }
    }

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
