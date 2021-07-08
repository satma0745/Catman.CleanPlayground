namespace Catman.CleanPlayground.Application.UseCases.Common.Request
{
    using Catman.CleanPlayground.Application.UseCases.Common.Response;
    using MediatR;

    public abstract class RequestBase<TResource> : IRequest<IResponse<TResource>>
    {
        public virtual bool RequireAuthorizedUser => false;
        
        public string AuthorizationToken { get; }

        protected RequestBase(string authorizationToken = default)
        {
            AuthorizationToken = authorizationToken;
        }
    }
}
