namespace Catman.CleanPlayground.WebApi.Extensions.Services
{
    using System;
    using System.Threading.Tasks;
    using Catman.CleanPlayground.Application.Extensions.UseCases;
    using Catman.CleanPlayground.Application.UseCases.Common.Response;
    using Catman.CleanPlayground.Application.UseCases.Common.Response.Errors;
    using Microsoft.AspNetCore.Mvc;

    internal static class OperationResultExtensions
    {
        public static async Task<IActionResult> SelectActionResultAsync<TResource>(
            this Task<OperationResult<TResource>> asyncOperationResult,
            Func<TResource, IActionResult> onSuccess,
            Func<Error, IActionResult> onFailure)
        {
            var operationResult = await asyncOperationResult;
            return operationResult.Select(onSuccess, onFailure);
        }

        public static async Task<IActionResult> SelectActionResultAsync(
            this Task<OperationResult<BlankResource>> asyncOperationResult,
            Func<IActionResult> onSuccess,
            Func<Error, IActionResult> onFailure)
        {
            var operationResult = await asyncOperationResult;
            return operationResult.Select(onSuccess, onFailure);
        }
    }
}
