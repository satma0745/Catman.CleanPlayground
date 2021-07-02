namespace Catman.CleanPlayground.Application.Services.Common.Response.Errors
{
    using System;

    public class FatalError : Error
    {
        public Exception Cause { get; }

        public FatalError(Exception exception)
            : base(exception.Message)
        {
            Cause = exception;
        }
    }
}
