namespace Catman.CleanPlayground.Application.Services.Common.Operation
{
    using System.Threading.Tasks;
    using Catman.CleanPlayground.Application.Services.Common.Response;

    public interface IOperation<in TRequest, TResource>
    {
        Task<OperationResult<TResource>> PerformAsync(TRequest request, string authenticationToken);
    }
}
