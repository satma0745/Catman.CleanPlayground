namespace Catman.CleanPlayground.Application.UseCases.Common.Response.Errors
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
