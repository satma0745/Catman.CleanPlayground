namespace Catman.CleanPlayground.JwtAuthentication.Session.Manager
{
    using System.Threading.Tasks;
    using AutoMapper;
    using Catman.CleanPlayground.Application.Persistence.UnitOfWork;
    using Catman.CleanPlayground.Application.Session;
    using Catman.CleanPlayground.JwtAuthentication.Configuration;
    using Catman.CleanPlayground.JwtAuthentication.Extensions.Token;
    using JWT.Builder;
    using JWT.Exceptions;

    internal class SessionGenerator
    {
        private readonly IUnitOfWork _work;
        private readonly IMapper _mapper;

        private readonly JwtBuilder _jwtBuilder;

        public SessionGenerator(
            JwtAuthenticationConfiguration configuration,
            IUnitOfWork work,
            IMapper mapper)
        {
            _work = work;
            _mapper = mapper;
            
            _jwtBuilder = JwtBuilder.Create()
                .WithAlgorithm(configuration.Algorithm)
                .WithSecret(configuration.SecretKey)
                .MustVerifySignature();
        }

        public async Task<ISessionGenerationResult> GenerateSessionAsync(string authorizationToken)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(authorizationToken))
                {
                    return SessionGenerationResult.ValidationFailed("No authentication token provided.");
                }
                
                var tokenPayload = _jwtBuilder.GetPayload(authorizationToken);
                if (!await _work.Users.UserExistsAsync(tokenPayload.UserId))
                {
                    return SessionGenerationResult.ValidationFailed("Authorization token owner does not exist.");
                }

                var userData = await _work.Users.GetUserAsync(tokenPayload.UserId);
                var applicationUser = _mapper.Map<ApplicationUser>(userData);

                var session = new SessionInfo(applicationUser);
                return SessionGenerationResult.Authorized(session);
            }
            catch (TokenExpiredException)
            {
                return SessionGenerationResult.ValidationFailed("Authorization token has expired.");
            }
            catch (SignatureVerificationException)
            {
                return SessionGenerationResult.ValidationFailed("A fake authorization token provided.");
            }
        }
    }
}
