namespace Catman.CleanPlayground.Application.Services.Common.Response
{
    public class OperationResult<TResource>
    {
        private readonly bool _success;
        private readonly TResource _resource;
        private readonly Error _error;

        public OperationResult(TResource resource)
        {
            _success = true;
            _resource = resource;
            _error = default;
        }

        public OperationResult(Error error)
        {
            _success = false;
            _resource = default;
            _error = error;
        }

        public (bool Success, TResource Resource, Error Error) Consume() =>
            (_success, _resource, _error);
    }

    public class OperationResult : OperationResult<Nothing>
    {
        public OperationResult()
            : base( new Nothing() )
        {
        }
        
        public OperationResult(Error error)
            : base(error)
        {
        }
    }
}
