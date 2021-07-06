namespace Catman.CleanPlayground.Application.Services.Users.UpdateUser
{
    using System;
    using Catman.CleanPlayground.Application.Services.Common.Request;

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
