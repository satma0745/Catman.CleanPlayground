namespace Catman.CleanPlayground.Application.UseCases.Common.Request
{
    public abstract class RequestBase : IRequest
    {
        public string AuthorizationToken { get; set; }

        protected RequestBase(string authorizationToken = default)
        {
            AuthorizationToken = authorizationToken;
        }
    }
}
