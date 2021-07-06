namespace Catman.CleanPlayground.Application.UseCases.Common.Response.Errors
{
    public class AccessViolationError : Error
    {
        public AccessViolationError(string message)
            : base(message)
        {
        }
    }
}
