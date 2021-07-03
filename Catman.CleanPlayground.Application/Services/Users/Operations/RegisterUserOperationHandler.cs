namespace Catman.CleanPlayground.Application.Services.Users.Operations
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper;
    using Catman.CleanPlayground.Application.Persistence.Users;
    using Catman.CleanPlayground.Application.Services.Common.Response;
    using Catman.CleanPlayground.Application.Services.Common.Response.Errors;
    using Catman.CleanPlayground.Application.Services.Users.Requests;
    using FluentValidation;

    internal class RegisterUserOperationHandler
    {
        private readonly IUserRepository _userRepository;
        private readonly IValidator<RegisterUserRequest> _requestValidator;
        private readonly IMapper _mapper;

        public RegisterUserOperationHandler(
            IUserRepository userRepository,
            IValidator<RegisterUserRequest> requestValidator,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _requestValidator = requestValidator;
            _mapper = mapper;
        }
        
        public async Task<OperationResult<OperationSuccess>> HandleAsync(RegisterUserRequest registerRequest)
        {
            try
            {
                var validationResult = await _requestValidator.ValidateAsync(registerRequest);
                if (!validationResult.IsValid)
                {
                    var validationError = new ValidationError(validationResult);
                    return new OperationResult<OperationSuccess>(validationError);
                }

                var checkParams = new UsernameAvailabilityCheckParameters(registerRequest.Username);
                if (!await _userRepository.UsernameIsAvailableAsync(checkParams))
                {
                    var validationMessages = new Dictionary<string, string>
                    {
                        {nameof(registerRequest.Username), "Already taken."}
                    };
                    var validationError = new ValidationError(validationMessages);
                    return new OperationResult<OperationSuccess>(validationError);
                }

                var createData = _mapper.Map<UserCreateData>(registerRequest);
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
