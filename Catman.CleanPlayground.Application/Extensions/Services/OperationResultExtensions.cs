namespace Catman.CleanPlayground.Application.Extensions.Services
{
    using System;
    using Catman.CleanPlayground.Application.Services.Common.Response;
    using Catman.CleanPlayground.Application.Services.Common.Response.Errors;

    public static class OperationResultExtensions
    {
        public static TSelected Select<TResource, TSelected>(
            this OperationResult<TResource> operationResult,
            Func<TResource, TSelected> onSuccess,
            Func<Error, TSelected> onFailure)
        {
            var (success, resource, error) = operationResult.Consume();
            return success
                ? onSuccess(resource)
                : onFailure(error);
        }

        public static TSelected Select<TSelected>(
            this OperationResult<OperationSuccess> operationResult,
            Func<TSelected> onSuccess,
            Func<Error, TSelected> onFailure) =>
            operationResult.Select(onSuccess: _ => onSuccess(), onFailure);
    }
}
