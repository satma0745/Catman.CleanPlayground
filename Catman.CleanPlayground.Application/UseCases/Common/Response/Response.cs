namespace Catman.CleanPlayground.Application.UseCases.Common.Response
{
    using Catman.CleanPlayground.Application.UseCases.Common.Response.Errors;

    internal class Response<TResource> : IResponse<TResource>
    {
        private readonly bool _success;
        private readonly TResource _resource;
        private readonly Error _error;

        public Response(TResource resource)
        {
            _success = true;
            _resource = resource;
            _error = default;
        }

        public Response(Error error)
        {
            _success = false;
            _resource = default;
            _error = error;
        }

        public (bool Success, TResource Resource, Error Error) Consume() =>
            (_success, _resource, _error);
    }
}
