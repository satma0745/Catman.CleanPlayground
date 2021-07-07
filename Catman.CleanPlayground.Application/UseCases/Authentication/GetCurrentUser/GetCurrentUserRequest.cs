namespace Catman.CleanPlayground.Application.UseCases.Authentication.GetCurrentUser
{
    using Catman.CleanPlayground.Application.UseCases.Common.Request;

    public class GetCurrentUserRequest : RequestBase<CurrentUserResource>
    {
        public GetCurrentUserRequest(string authorizationToken)
            : base(authorizationToken)
        {
        }
    }
}
