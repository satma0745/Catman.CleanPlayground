namespace Catman.CleanPlayground.WebApi.Extensions.Services
{
    using System;
    using System.Threading.Tasks;
    using Catman.CleanPlayground.Application.Extensions.UseCases;
    using Catman.CleanPlayground.Application.UseCases.Common.Response;
    using Catman.CleanPlayground.Application.UseCases.Common.Response.Errors;
    using Microsoft.AspNetCore.Mvc;

    internal static class ResponseExtensions
    {
        public static async Task<IActionResult> SelectActionResultAsync<TResource>(
            this Task<IResponse<TResource>> asyncResponse,
            Func<TResource, IActionResult> onSuccess,
            Func<Error, IActionResult> onFailure)
        {
            var response = await asyncResponse;
            return response.Select(onSuccess, onFailure);
        }

        public static async Task<IActionResult> SelectActionResultAsync(
            this Task<IResponse<BlankResource>> asyncResponse,
            Func<IActionResult> onSuccess,
            Func<Error, IActionResult> onFailure)
        {
            var response = await asyncResponse;
            return response.Select(onSuccess, onFailure);
        }
    }
}
