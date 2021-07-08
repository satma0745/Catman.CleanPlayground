namespace Catman.CleanPlayground.Application.UseCases.Common.RequestHandling
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;
    using Catman.CleanPlayground.Application.UseCases.Common.Request;
    using Catman.CleanPlayground.Application.UseCases.Common.Response;
    using MediatR;

    internal abstract class PipelineBehaviorBase<TAnyRequest, TResponse> : IPipelineBehavior<TAnyRequest, TResponse>
    {
        private static bool IsSupportedResponseType =>
            typeof(TResponse).IsInterface &&
            typeof(TResponse).IsGenericType &&
            typeof(TResponse).GetGenericTypeDefinition() == typeof(IResponse<>);
        
        public Task<TResponse> Handle(
            TAnyRequest request,
            CancellationToken _,
            RequestHandlerDelegate<TResponse> next)
        {
            if (!IsSupportedResponseType)
            {
                throw new Exception("Unsupported response type.");
            }
            
            return (Task<TResponse>) GetType()
                .GetMethod(nameof(HandleAsync), BindingFlags.Instance | BindingFlags.NonPublic)!
                .MakeGenericMethod(typeof(TAnyRequest), typeof(TResponse).GenericTypeArguments.Single())
                .Invoke(this, new object[] {request, next});
        }

        protected abstract Task<IResponse<TResource>> HandleAsync<TRequest, TResource>(
            TRequest request,
            RequestHandlerDelegate<IResponse<TResource>> next)
            where TRequest : RequestBase<TResource>, TAnyRequest;
    }
}
