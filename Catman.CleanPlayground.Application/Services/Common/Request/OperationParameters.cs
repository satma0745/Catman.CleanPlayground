namespace Catman.CleanPlayground.Application.Services.Common.Request
{
    public class OperationParameters<TRequest>
    {
        public bool Authorized { get; init; }
        
        public ApplicationUser CurrentUser { get; init; }
        
        public TRequest Request { get; init; }
    }
}
