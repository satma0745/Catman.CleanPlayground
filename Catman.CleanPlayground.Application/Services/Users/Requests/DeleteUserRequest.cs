namespace Catman.CleanPlayground.Application.Services.Users.Requests
{
    using System;
    using Catman.CleanPlayground.Application.Services.Common.Request;
    using FluentValidation;

    public class DeleteUserRequest : RequestBase
    {
        public Guid Id { get; }

        public DeleteUserRequest(Guid id, string authorizationToken)
            : base(authorizationToken)
        {
            Id = id;
        }
    }

    internal class DeleteUserRequestValidator : AbstractValidator<DeleteUserRequest>
    {
        public DeleteUserRequestValidator()
        {
            RuleFor(request => request.Id).NotEmpty();
        }
    }
}
