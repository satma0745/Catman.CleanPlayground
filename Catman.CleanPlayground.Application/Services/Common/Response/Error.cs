namespace Catman.CleanPlayground.Application.Services.Common.Response
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
