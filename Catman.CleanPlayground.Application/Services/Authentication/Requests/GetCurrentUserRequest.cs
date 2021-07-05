namespace Catman.CleanPlayground.Application.Services.Authentication.Requests
{
    using Catman.CleanPlayground.Application.Services.Common.Request;

    public class GetCurrentUserRequest : RequestBase
    {
        public GetCurrentUserRequest(string authorizationToken)
            : base(authorizationToken)
        {
        }
    }
}
