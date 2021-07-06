namespace Catman.CleanPlayground.Application.UseCases.Users.UpdateUser
{
    using System;
    using Catman.CleanPlayground.Application.UseCases.Common.Request;

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
}
