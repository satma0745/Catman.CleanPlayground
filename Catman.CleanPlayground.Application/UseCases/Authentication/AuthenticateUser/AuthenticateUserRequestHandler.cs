namespace Catman.CleanPlayground.Application.UseCases.Authentication.AuthenticateUser
{
    using System.Threading.Tasks;
    using Catman.CleanPlayground.Application.Helpers.AuthorizationToken;
    using Catman.CleanPlayground.Application.Helpers.Password;
    using Catman.CleanPlayground.Application.Localization;
    using Catman.CleanPlayground.Application.Persistence.UnitOfWork;
    using Catman.CleanPlayground.Application.UseCases.Common.RequestHandling;
    using Catman.CleanPlayground.Application.UseCases.Common.Response;

    internal class AuthenticateUserRequestHandler : RequestHandlerBase<AuthenticateUserRequest, string>
    {
        private readonly ITokenHelper _tokenHelper;
        private readonly IPasswordHelper _passwordHelper;
        private readonly IUserLocalizer _localizer;
        private readonly IUnitOfWork _work;

        public AuthenticateUserRequestHandler(
            ITokenHelper tokenHelper,
            IUnitOfWork unitOfWork,
            IUserLocalizer localizer,
            IPasswordHelper passwordHelper)
        {
            _tokenHelper = tokenHelper;
            _passwordHelper = passwordHelper;
            _localizer = localizer;
            _work = unitOfWork;
        }
        
        protected override async Task<Response<string>> HandleAsync(AuthenticateUserRequest request)
        {
            if (!await _work.Users.UserExistsAsync(request.Username))
            {
                return NotFound(_localizer.NotFound());
            }

            var user = await _work.Users.GetUserAsync(request.Username);

            var hashedPassword = _passwordHelper.HashPassword(request.Password, user.Password.Hash);
            if (user.Password.Hash != hashedPassword)
            {
                return ValidationFailed(nameof(request.Password), _localizer.IncorrectPassword());
            }

            var authorizationToken = _tokenHelper.GenerateToken(user.Id);
            return Success(authorizationToken);
        }
    }
}
