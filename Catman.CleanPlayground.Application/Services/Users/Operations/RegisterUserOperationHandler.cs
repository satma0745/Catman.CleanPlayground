namespace Catman.CleanPlayground.Application.Services.Users.Operations
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper;
    using Catman.CleanPlayground.Application.Persistence.Users;
    using Catman.CleanPlayground.Application.Services.Common.Response;
    using Catman.CleanPlayground.Application.Services.Common.Response.Errors;
    using Catman.CleanPlayground.Application.Services.Users.Models;
    using FluentValidation;

    internal class RegisterUserOperationHandler
    {
        private readonly IUserRepository _userRepository;
        private readonly IValidator<RegisterUserModel> _modelValidator;
        private readonly IMapper _mapper;

        public RegisterUserOperationHandler(
            IUserRepository userRepository,
            IValidator<RegisterUserModel> modelValidator,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _modelValidator = modelValidator;
            _mapper = mapper;
        }
        
        public async Task<OperationResult<OperationSuccess>> HandleAsync(RegisterUserModel registerModel)
        {
            try
            {
                var validationResult = await _modelValidator.ValidateAsync(registerModel);
                if (!validationResult.IsValid)
                {
                    var validationError = new ValidationError(validationResult);
                    return new OperationResult<OperationSuccess>(validationError);
                }

                var checkParams = new UsernameAvailabilityCheckParameters(registerModel.Username);
                if (!await _userRepository.UsernameIsAvailableAsync(checkParams))
                {
                    var validationMessages = new Dictionary<string, string>
                    {
                        {nameof(registerModel.Username), "Already taken."}
                    };
                    var validationError = new ValidationError(validationMessages);
                    return new OperationResult<OperationSuccess>(validationError);
                }

                var createData = _mapper.Map<UserCreateData>(registerModel);
                await _userRepository.CreateUserAsync(createData);

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
