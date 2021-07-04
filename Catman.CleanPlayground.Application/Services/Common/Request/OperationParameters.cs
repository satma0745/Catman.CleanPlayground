namespace Catman.CleanPlayground.Application.Services.Common.Request
{
    using Catman.CleanPlayground.Application.Session;

    public class OperationParameters<TRequest>
        where TRequest : RequestBase
    {
        public ISession Session { get; init; }
        
        public TRequest Request { get; init; }
    }
}
