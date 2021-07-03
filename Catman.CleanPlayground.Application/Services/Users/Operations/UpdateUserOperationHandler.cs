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
    using Catman.CleanPlayground.Application.Services.Users.Models;
    using FluentValidation;

    internal class UpdateUserOperationHandler
    {
        private readonly IUserRepository _userRepository;
        private readonly IValidator<UpdateUserModel> _modelValidator;
        private readonly TokenManager _tokenManager;
        private readonly IMapper _mapper;

        public UpdateUserOperationHandler(
            IUserRepository userRepository,
            IValidator<UpdateUserModel> modelValidator,
            TokenManager tokenManager,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _modelValidator = modelValidator;
            _tokenManager = tokenManager;
            _mapper = mapper;
        }
        
        public async Task<OperationResult<OperationSuccess>> HandleAsync(UpdateUserModel updateModel)
        {
            try
            {
                var validationResult = await _modelValidator.ValidateAsync(updateModel);
                if (!validationResult.IsValid)
                {
                    var validationError = new ValidationError(validationResult);
                    return new OperationResult<OperationSuccess>(validationError);
                }

                var authenticationResult = await _tokenManager.AuthenticateTokenAsync(updateModel.AuthenticationToken);
                if (!authenticationResult.IsValid)
                {
                    var authenticationError = new AuthenticationError(authenticationResult.ErrorMessage);
                    return new OperationResult<OperationSuccess>(authenticationError);
                }

                if (updateModel.Id != authenticationResult.UserId)
                {
                    var accessViolationError = new AccessViolationError("You can only edit your own profile.");
                    return new OperationResult<OperationSuccess>(accessViolationError);
                }
                
                if (!await _userRepository.UserExistsAsync(updateModel.Id))
                {
                    var notFoundError = new NotFoundError("User not found.");
                    return new OperationResult<OperationSuccess>(notFoundError);
                }

                var checkParameters = new UsernameAvailabilityCheckParameters(updateModel.Username, updateModel.Id);
                if (!await _userRepository.UsernameIsAvailableAsync(checkParameters))
                {
                    var validationMessages = new Dictionary<string, string>
                    {
                        {nameof(updateModel.Username), "Already taken."}
                    };
                    var validationError = new ValidationError(validationMessages);
                    return new OperationResult<OperationSuccess>(validationError);
                }

                var updateData = _mapper.Map<UserUpdateData>(updateModel);
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
