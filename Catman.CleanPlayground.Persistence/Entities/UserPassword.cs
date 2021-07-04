namespace Catman.CleanPlayground.Persistence.Entities
{
    internal class UserPassword
    {
        public string Hash { get; init; }
        
        public string Salt { get; init; }
    }
}
