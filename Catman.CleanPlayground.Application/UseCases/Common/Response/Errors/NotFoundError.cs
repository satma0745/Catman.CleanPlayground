namespace Catman.CleanPlayground.Application.UseCases.Common.Response.Errors
{
    public class NotFoundError : Error
    {
        public NotFoundError(string message)
            : base(message)
        {
        }
    }
}
