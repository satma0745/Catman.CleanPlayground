namespace Catman.CleanPlayground.Application.Session
{
    public interface ISessionGenerationResult
    {
        bool Success { get; }
        
        ISession Session { get; }
        
        string ValidationError { get; }
    }
}
