namespace Catman.CleanPlayground.Application.Services.Common.Response.Errors
{
    public class ConflictError : Error
    {
        public ConflictError(string message)
            : base(message)
        {
        }
    }
}
