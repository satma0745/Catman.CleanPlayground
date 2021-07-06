namespace Catman.CleanPlayground.Application.UseCases.Common.Response
{
    using Catman.CleanPlayground.Application.UseCases.Common.Response.Errors;

    public interface IResponse<TResource>
    {
        (bool Success, TResource Resource, Error Error) Consume();
    }
}
