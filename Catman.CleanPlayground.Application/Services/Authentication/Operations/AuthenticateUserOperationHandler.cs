namespace Catman.CleanPlayground.Application.Services.Authentication.Operations
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Catman.CleanPlayground.Application.Authentication;
    using Catman.CleanPlayground.Application.Persistence.Users;
    using Catman.CleanPlayground.Application.Services.Authentication.Models;
    using Catman.CleanPlayground.Application.Services.Common.Response;
    using Catman.CleanPlayground.Application.Services.Common.Response.Errors;
    using FluentValidation;

    internal class AuthenticateUserOperationHandler
    {
        private readonly IUserRepository _userRepository;
        private readonly IValidator<UserCredentialsModel> _modelValidator;
        private readonly ITokenManager _tokenManager;

        public AuthenticateUserOperationHandler(
            IUserRepository userRepository,
            IValidator<UserCredentialsModel> modelValidator,
            ITokenManager tokenManager)
        {
            _userRepository = userRepository;
            _modelValidator = modelValidator;
            _tokenManager = tokenManager;
        }
        
        public async Task<OperationResult<string>> HandleAsync(UserCredentialsModel credentialsModel)
        {
            var modelValidationResult = await _modelValidator.ValidateAsync(credentialsModel);
            if (!modelValidationResult.IsValid)
            {
                var validationError = new ValidationError(modelValidationResult);
                return new OperationResult<string>(validationError);
            }
            
            if (!await _userRepository.UserExistsAsync(credentialsModel.Username))
            {
                var notFoundError = new NotFoundError("User not found.");
                return new OperationResult<string>(notFoundError);
            }

            var user = await _userRepository.GetUserAsync(credentialsModel.Username);
            if (!await _userRepository.UserHasPasswordAsync(user.Id, credentialsModel.Password))
            {
                var validationMessages = new Dictionary<string, string>
                {
                    {nameof(credentialsModel.Password), "Incorrect password."}
                };
                var validationError = new ValidationError(validationMessages);
                return new OperationResult<string>(validationError);
            }

            var token = _tokenManager.GenerateToken(user.Id);
            return new OperationResult<string>(token);
        }
    }
}
