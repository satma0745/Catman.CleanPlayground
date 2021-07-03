namespace Catman.CleanPlayground.Application.Services.Common.Response.Errors
{
    public class AccessViolationError : Error
    {
        public AccessViolationError(string message)
            : base(message)
        {
        }
    }
}
