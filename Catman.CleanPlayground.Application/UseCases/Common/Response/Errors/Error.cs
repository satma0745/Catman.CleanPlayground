namespace Catman.CleanPlayground.Application.UseCases.Common.Response.Errors
{
    public class Error
    {
        public virtual string Message { get; }

        public Error(string message)
        {
            Message = message;
        }
    }
}
