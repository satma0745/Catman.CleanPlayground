namespace Catman.CleanPlayground.Application.UseCases.Authentication.AuthenticateUser
{
    using System.Threading.Tasks;
    using Catman.CleanPlayground.Application.Helpers.Password;
    using Catman.CleanPlayground.Application.Persistence.UnitOfWork;
    using Catman.CleanPlayground.Application.Session;
    using Catman.CleanPlayground.Application.UseCases.Common.RequestHandling;
    using Catman.CleanPlayground.Application.UseCases.Common.Response;

    internal class AuthenticateUserRequestHandler : RequestHandlerBase<AuthenticateUserRequest, string>
    {
        private readonly ISessionManager _sessionManager;
        private readonly IPasswordHelper _passwordHelper;
        private readonly IUnitOfWork _work;

        public AuthenticateUserRequestHandler(
            ISessionManager sessionManager,
            IUnitOfWork unitOfWork,
            IPasswordHelper passwordHelper)
            : base(sessionManager)
        {
            _sessionManager = sessionManager;
            _passwordHelper = passwordHelper;
            _work = unitOfWork;
        }
        
        protected override async Task<Response<string>> HandleAsync(AuthenticateUserRequest request)
        {
            if (!await _work.Users.UserExistsAsync(request.Username))
            {
                return NotFound("User not found.");
            }

            var user = await _work.Users.GetUserAsync(request.Username);

            var hashedPassword = _passwordHelper.HashPassword(request.Password, user.Password.Hash);
            if (user.Password.Hash != hashedPassword)
            {
                return ValidationFailed(nameof(request.Password), "Incorrect password.");
            }

            var authorizationToken = _sessionManager.GenerateAuthorizationToken(user.Id);
            return Success(authorizationToken);
        }
    }
}
