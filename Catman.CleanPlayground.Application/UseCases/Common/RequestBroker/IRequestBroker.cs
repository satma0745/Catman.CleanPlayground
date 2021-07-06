namespace Catman.CleanPlayground.Application.UseCases.Common.RequestBroker
{
    using System.Threading.Tasks;
    using Catman.CleanPlayground.Application.UseCases.Common.Request;
    using Catman.CleanPlayground.Application.UseCases.Common.Response;

    public interface IRequestBroker
    {
        Task<IResponse<TResource>> SendRequestAsync<TRequest, TResource>(TRequest request)
            where TRequest : IRequest;
    }
}
