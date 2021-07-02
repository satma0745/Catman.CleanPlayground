namespace Catman.CleanPlayground.Application.Services.Users.Operations
{
    using System;
    using System.Threading.Tasks;
    using AutoMapper;
    using Catman.CleanPlayground.Application.Persistence.Users;
    using Catman.CleanPlayground.Application.Services.Common.Response;
    using Catman.CleanPlayground.Application.Services.Common.Response.Errors;
    using Catman.CleanPlayground.Application.Services.Users.Models;

    internal class RegisterUserOperationHandler
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public RegisterUserOperationHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        
        public async Task<OperationResult> HandleAsync(RegisterUserModel registerModel)
        {
            try
            {
                var checkParams = new UsernameAvailabilityCheckParameters(registerModel.Username);
                if (!await _userRepository.UsernameIsAvailableAsync(checkParams))
                {
                    var conflictError = new ConflictError("Username already taken.");
                    return new OperationResult(conflictError);
                }

                var createData = _mapper.Map<UserCreateData>(registerModel);
                await _userRepository.CreateUserAsync(createData);

                return new OperationResult();
            }
            catch (Exception exception)
            {
                var fatalError = new FatalError(exception);
                return new OperationResult(fatalError);
            }
        }
    }
}
