namespace Catman.CleanPlayground.Application.Services.Common.Response.Errors
{
    public class AuthenticationError : Error
    {
        public AuthenticationError(string message)
            : base(message)
        {
        }
    }
}
