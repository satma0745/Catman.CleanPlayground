namespace Catman.CleanPlayground.Application.Services.Users.Operations
{
    using System;
    using System.Threading.Tasks;
    using Catman.CleanPlayground.Application.Authentication;
    using Catman.CleanPlayground.Application.Persistence.Users;
    using Catman.CleanPlayground.Application.Services.Common.Response;
    using Catman.CleanPlayground.Application.Services.Common.Response.Errors;
    using Catman.CleanPlayground.Application.Services.Users.Requests;
    using FluentValidation;

    internal class DeleteUserOperationHandler
    {
        private readonly IUserRepository _userRepository;
        private readonly IValidator<DeleteUserRequest> _requestValidator;
        private readonly ITokenManager _tokenManager;

        public DeleteUserOperationHandler(
            IUserRepository userRepository,
            IValidator<DeleteUserRequest> requestValidator,
            ITokenManager tokenManager)
        {
            _userRepository = userRepository;
            _requestValidator = requestValidator;
            _tokenManager = tokenManager;
        }

        public async Task<OperationResult<OperationSuccess>> HandleAsync(DeleteUserRequest deleteRequest)
        {
            try
            {
                var validationResult = await _requestValidator.ValidateAsync(deleteRequest);
                if (!validationResult.IsValid)
                {
                    var validationError = new ValidationError(validationResult);
                    return new OperationResult<OperationSuccess>(validationError);
                }
                
                var authenticationResult = _tokenManager.AuthenticateToken(deleteRequest.AuthenticationToken);
                if (!authenticationResult.IsValid ||
                    !await _userRepository.UserExistsAsync(authenticationResult.UserId!.Value))
                {
                    var authenticationError = new AuthenticationError(authenticationResult.ErrorMessage);
                    return new OperationResult<OperationSuccess>(authenticationError);
                }

                if (deleteRequest.Id != authenticationResult.UserId)
                {
                    var accessViolationError = new AccessViolationError("You can only delete your own profile.");
                    return new OperationResult<OperationSuccess>(accessViolationError);
                }
                
                if (!await _userRepository.UserExistsAsync(deleteRequest.Id))
                {
                    var notFoundError = new NotFoundError("User not found.");
                    return new OperationResult<OperationSuccess>(notFoundError);
                }
            
                await _userRepository.RemoveUserAsync(deleteRequest.Id);
                
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
