namespace Catman.CleanPlayground.Application.UseCases.Common.RequestHandler
{
    using System.Threading.Tasks;
    using Catman.CleanPlayground.Application.UseCases.Common.Request;
    using Catman.CleanPlayground.Application.UseCases.Common.Response;

    public interface IRequestHandler<in TRequest, TResource>
        where TRequest : IRequest
    {
        Task<IResponse<TResource>> PerformAsync(TRequest request);
    }
}
