namespace Catman.CleanPlayground.Application.UseCases.Users.DeleteUser
{
    using System;
    using Catman.CleanPlayground.Application.UseCases.Common.Request;
    using Catman.CleanPlayground.Application.UseCases.Common.Response;

    public class DeleteUserRequest : RequestBase<BlankResource>
    {
        public Guid Id { get; }

        public DeleteUserRequest(Guid id, string authorizationToken)
            : base(authorizationToken)
        {
            Id = id;
        }
    }
}
