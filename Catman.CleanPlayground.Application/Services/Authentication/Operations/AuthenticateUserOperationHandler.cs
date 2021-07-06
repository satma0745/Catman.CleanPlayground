namespace Catman.CleanPlayground.Application.Services.Authentication.Operations
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Catman.CleanPlayground.Application.Helpers.Password;
    using Catman.CleanPlayground.Application.Persistence.UnitOfWork;
    using Catman.CleanPlayground.Application.Services.Authentication.Requests;
    using Catman.CleanPlayground.Application.Services.Common.Operation.Handler;
    using Catman.CleanPlayground.Application.Services.Common.Response;
    using Catman.CleanPlayground.Application.Session;
    using FluentValidation;

    internal class AuthenticateUserOperationHandler : OperationHandlerBase<AuthenticateUserRequest, string>
    {
        private readonly ISessionManager _sessionManager;
        private readonly IPasswordHelper _passwordHelper;
        private readonly IUnitOfWork _work;

        public AuthenticateUserOperationHandler(
            IEnumerable<IValidator<AuthenticateUserRequest>> requestValidators,
            ISessionManager sessionManager,
            IUnitOfWork unitOfWork,
            IPasswordHelper passwordHelper)
            : base(requestValidators, sessionManager)
        {
            _sessionManager = sessionManager;
            _passwordHelper = passwordHelper;
            _work = unitOfWork;
        }
        
        protected override async Task<OperationResult<string>> HandleRequestAsync(AuthenticateUserRequest request)
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
