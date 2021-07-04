namespace Catman.CleanPlayground.Application.Services.Authentication.Operations
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Catman.CleanPlayground.Application.Persistence.Users;
    using Catman.CleanPlayground.Application.Services.Authentication.Requests;
    using Catman.CleanPlayground.Application.Services.Common.Operation;
    using Catman.CleanPlayground.Application.Services.Common.Request;
    using Catman.CleanPlayground.Application.Services.Common.Response;
    using Catman.CleanPlayground.Application.Services.Common.Response.Errors;
    using Catman.CleanPlayground.Application.Session;
    using FluentValidation;

    internal class AuthenticateUserOperationHandler : OperationHandlerBase<AuthenticateUserRequest, string>
    {
        private readonly IUserRepository _userRepository;
        private readonly ISessionManager _sessionManager;

        public AuthenticateUserOperationHandler(
            IUserRepository userRepository,
            IEnumerable<IValidator<AuthenticateUserRequest>> requestValidators,
            ISessionManager sessionManager)
            : base(requestValidators, sessionManager)
        {
            _userRepository = userRepository;
            _sessionManager = sessionManager;
        }
        
        protected override async Task<OperationResult<string>> HandleRequestAsync(
            OperationParameters<AuthenticateUserRequest> parameters)
        {
            if (!await _userRepository.UserExistsAsync(parameters.Request.Username))
            {
                var notFoundError = new NotFoundError("User not found.");
                return new OperationResult<string>(notFoundError);
            }

            var user = await _userRepository.GetUserAsync(parameters.Request.Username);
            if (!await _userRepository.UserHasPasswordAsync(user.Id, parameters.Request.Password))
            {
                var validationMessages = new Dictionary<string, string>
                {
                    {nameof(parameters.Request.Password), "Incorrect password."}
                };
                var validationError = new ValidationError(validationMessages);
                return new OperationResult<string>(validationError);
            }

            var authorizationToken = _sessionManager.GenerateAuthorizationToken(user.Id);
            return new OperationResult<string>(authorizationToken);
        }
    }
}
