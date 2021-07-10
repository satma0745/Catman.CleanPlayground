namespace Catman.CleanPlayground.JwtAuthentication.Configuration
{
    using System.Collections.Generic;
    using JWT.Algorithms;

    internal class JwtAuthenticationConfiguration
    {
        public string SecretKey { get; init; }
        
        public int TokenLifetimeInDays { get; init; }
        
        public IJwtAlgorithm Algorithm { get; init; }
        
        public ICollection<string> TokenPrefixes { get; init; }
        
        public byte TokenVersion { get; init; }
    }
}
