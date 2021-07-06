namespace Catman.CleanPlayground.Application.Extensions.UseCases
{
    using System;
    using Catman.CleanPlayground.Application.UseCases.Common.Response;
    using Catman.CleanPlayground.Application.UseCases.Common.Response.Errors;

    public static class ResponseExtensions
    {
        public static TSelected Select<TResource, TSelected>(
            this IResponse<TResource> response,
            Func<TResource, TSelected> onSuccess,
            Func<Error, TSelected> onFailure)
        {
            var (success, resource, error) = response.Consume();
            return success
                ? onSuccess(resource)
                : onFailure(error);
        }

        public static TSelected Select<TSelected>(
            this IResponse<BlankResource> response,
            Func<TSelected> onSuccess,
            Func<Error, TSelected> onFailure) =>
            response.Select(_ => onSuccess(), onFailure);
    }
}
