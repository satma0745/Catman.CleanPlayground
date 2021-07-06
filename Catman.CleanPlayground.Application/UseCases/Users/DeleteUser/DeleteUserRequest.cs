namespace Catman.CleanPlayground.Application.UseCases.Users.DeleteUser
{
    using System;
    using Catman.CleanPlayground.Application.UseCases.Common.Request;

    public class DeleteUserRequest : RequestBase
    {
        public Guid Id { get; }

        public DeleteUserRequest(Guid id, string authorizationToken)
            : base(authorizationToken)
        {
            Id = id;
        }
    }
}
