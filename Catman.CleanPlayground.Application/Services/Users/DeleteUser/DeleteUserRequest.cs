namespace Catman.CleanPlayground.Application.Services.Users.DeleteUser
{
    using System;
    using Catman.CleanPlayground.Application.Services.Common.Request;

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
