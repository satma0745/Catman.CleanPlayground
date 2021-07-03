namespace Catman.CleanPlayground.Application.Services.Users.Operations
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper;
    using Catman.CleanPlayground.Application.Authentication;
    using Catman.CleanPlayground.Application.Persistence.Users;
    using Catman.CleanPlayground.Application.Services.Common.Response;
    using Catman.CleanPlayground.Application.Services.Common.Response.Errors;
    using Catman.CleanPlayground.Application.Services.Users.Requests;
    using FluentValidation;

    internal class UpdateUserOperationHandler
    {
        private readonly IUserRepository _userRepository;
        private readonly IValidator<UpdateUserRequest> _requestValidator;
        private readonly ITokenManager _tokenManager;
        private readonly IMapper _mapper;

        public UpdateUserOperationHandler(
            IUserRepository userRepository,
            IValidator<UpdateUserRequest> requestValidator,
            ITokenManager tokenManager,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _requestValidator = requestValidator;
            _tokenManager = tokenManager;
            _mapper = mapper;
        }
        
        public async Task<OperationResult<OperationSuccess>> HandleAsync(UpdateUserRequest updateRequest)
        {
            try
            {
                var validationResult = await _requestValidator.ValidateAsync(updateRequest);
                if (!validationResult.IsValid)
                {
                    var validationError = new ValidationError(validationResult);
                    return new OperationResult<OperationSuccess>(validationError);
                }

                var authenticationResult = _tokenManager.AuthenticateToken(updateRequest.AuthenticationToken);
                if (!authenticationResult.IsValid ||
                    !await _userRepository.UserExistsAsync(authenticationResult.UserId!.Value))
                {
                    var authenticationError = new AuthenticationError(authenticationResult.ErrorMessage);
                    return new OperationResult<OperationSuccess>(authenticationError);
                }

                if (updateRequest.Id != authenticationResult.UserId)
                {
                    var accessViolationError = new AccessViolationError("You can only edit your own profile.");
                    return new OperationResult<OperationSuccess>(accessViolationError);
                }
                
                if (!await _userRepository.UserExistsAsync(updateRequest.Id))
                {
                    var notFoundError = new NotFoundError("User not found.");
                    return new OperationResult<OperationSuccess>(notFoundError);
                }

                var checkParameters = new UsernameAvailabilityCheckParameters(updateRequest.Username, updateRequest.Id);
                if (!await _userRepository.UsernameIsAvailableAsync(checkParameters))
                {
                    var validationMessages = new Dictionary<string, string>
                    {
                        {nameof(updateRequest.Username), "Already taken."}
                    };
                    var validationError = new ValidationError(validationMessages);
                    return new OperationResult<OperationSuccess>(validationError);
                }

                var updateData = _mapper.Map<UserUpdateData>(updateRequest);
                await _userRepository.UpdateUserAsync(updateData);

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
