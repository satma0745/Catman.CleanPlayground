namespace Catman.CleanPlayground.Application.Services.Users.Operations
{
    using System;
    using System.Threading.Tasks;
    using Catman.CleanPlayground.Application.Authentication;
    using Catman.CleanPlayground.Application.Persistence.Users;
    using Catman.CleanPlayground.Application.Services.Common.Response;
    using Catman.CleanPlayground.Application.Services.Common.Response.Errors;
    using Catman.CleanPlayground.Application.Services.Users.Models;
    using FluentValidation;

    internal class DeleteUserOperationHandler
    {
        private readonly IUserRepository _userRepository;
        private readonly IValidator<DeleteUserModel> _modelValidator;
        private readonly TokenManager _tokenManager;

        public DeleteUserOperationHandler(
            IUserRepository userRepository,
            IValidator<DeleteUserModel> modelValidator,
            TokenManager tokenManager)
        {
            _userRepository = userRepository;
            _modelValidator = modelValidator;
            _tokenManager = tokenManager;
        }

        public async Task<OperationResult<OperationSuccess>> HandleAsync(DeleteUserModel deleteModel)
        {
            try
            {
                var validationResult = await _modelValidator.ValidateAsync(deleteModel);
                if (!validationResult.IsValid)
                {
                    var validationError = new ValidationError(validationResult);
                    return new OperationResult<OperationSuccess>(validationError);
                }
                
                var authenticationResult = await _tokenManager.AuthenticateTokenAsync(deleteModel.AuthenticationToken);
                if (!authenticationResult.IsValid)
                {
                    var authenticationError = new AuthenticationError(authenticationResult.ErrorMessage);
                    return new OperationResult<OperationSuccess>(authenticationError);
                }

                if (deleteModel.Id != authenticationResult.UserId)
                {
                    var accessViolationError = new AccessViolationError("You can only delete your own profile.");
                    return new OperationResult<OperationSuccess>(accessViolationError);
                }
                
                if (!await _userRepository.UserExistsAsync(deleteModel.Id))
                {
                    var notFoundError = new NotFoundError("User not found.");
                    return new OperationResult<OperationSuccess>(notFoundError);
                }
            
                await _userRepository.RemoveUserAsync(deleteModel.Id);
                
                return new OperationResult<OperationSuccess>(new OperationSuccess());
            }
            catch (Exception exception)
            {
                var fatalError = new FatalError(exception);
                return new OperationResult<OperationSuccess>(fatalError);
            }
        }
    }
}
