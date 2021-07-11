namespace Catman.CleanPlayground.WebApi.Extensions.UseCases
{
    using System;
    using System.Threading.Tasks;
    using MediatR;

    internal static class MediatorExtensions
    {
        public static Task<TResponse> Send<TResponse>(this IMediator mediator, Func<IRequest<TResponse>> requestBuilder)
        {
            var request = requestBuilder();
            return mediator.Send(request);
        }
    }
}
