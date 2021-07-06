namespace Catman.CleanPlayground.Application.UseCases.Common.Response.Errors
{
    public class AuthenticationError : Error
    {
        public AuthenticationError(string message)
            : base(message)
        {
        }
    }
}
