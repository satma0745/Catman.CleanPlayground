namespace Catman.CleanPlayground.Application.Services.Authentication.Operations
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Catman.CleanPlayground.Application.Authentication;
    using Catman.CleanPlayground.Application.Persistence.Users;
    using Catman.CleanPlayground.Application.Services.Authentication.Requests;
    using Catman.CleanPlayground.Application.Services.Common.Response;
    using Catman.CleanPlayground.Application.Services.Common.Response.Errors;
    using FluentValidation;

    internal class AuthenticateUserOperationHandler
    {
        private readonly IUserRepository _userRepository;
        private readonly IValidator<AuthenticateUserRequest> _requestValidator;
        private readonly ITokenManager _tokenManager;

        public AuthenticateUserOperationHandler(
            IUserRepository userRepository,
            IValidator<AuthenticateUserRequest> requestValidator,
            ITokenManager tokenManager)
        {
            _userRepository = userRepository;
            _requestValidator = requestValidator;
            _tokenManager = tokenManager;
        }
        
        public async Task<OperationResult<string>> HandleAsync(AuthenticateUserRequest request)
        {
            var validationResult = await _requestValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                var validationError = new ValidationError(validationResult);
                return new OperationResult<string>(validationError);
            }
            
            if (!await _userRepository.UserExistsAsync(request.Username))
            {
                var notFoundError = new NotFoundError("User not found.");
                return new OperationResult<string>(notFoundError);
            }

            var user = await _userRepository.GetUserAsync(request.Username);
            if (!await _userRepository.UserHasPasswordAsync(user.Id, request.Password))
            {
                var validationMessages = new Dictionary<string, string>
                {
                    {nameof(request.Password), "Incorrect password."}
                };
                var validationError = new ValidationError(validationMessages);
                return new OperationResult<string>(validationError);
            }

            var token = _tokenManager.GenerateToken(user.Id);
            return new OperationResult<string>(token);
        }
    }
}
