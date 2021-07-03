namespace Catman.CleanPlayground.JwtAuthentication.Token
{
    using JWT.Algorithms;

    internal class JwtAuthenticationConfiguration
    {
        public string SecretKey { get; init; }
        
        public int TokenLifetimeInDays { get; init; }
        
        public IJwtAlgorithm Algorithm { get; init; }
    }
}
