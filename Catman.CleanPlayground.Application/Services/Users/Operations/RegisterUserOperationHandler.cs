namespace Catman.CleanPlayground.Application.Services.Users.Operations
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper;
    using Catman.CleanPlayground.Application.Helpers.Password;
    using Catman.CleanPlayground.Application.Persistence.Users;
    using Catman.CleanPlayground.Application.Services.Common.Operation.Handler;
    using Catman.CleanPlayground.Application.Services.Common.Request;
    using Catman.CleanPlayground.Application.Services.Common.Response;
    using Catman.CleanPlayground.Application.Services.Users.Requests;
    using Catman.CleanPlayground.Application.Session;
    using FluentValidation;

    internal class RegisterUserOperationHandler : OperationHandlerBase<RegisterUserRequest, BlankResource>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHelper _passwordHelper;
        private readonly IMapper _mapper;

        public RegisterUserOperationHandler(
            IEnumerable<IValidator<RegisterUserRequest>> requestValidators,
            IUserRepository userRepository,
            ISessionManager sessionManager,
            IPasswordHelper passwordHelper,
            IMapper mapper)
            : base(requestValidators, sessionManager)
        {
            _userRepository = userRepository;
            _passwordHelper = passwordHelper;
            _mapper = mapper;
        }

        protected override async Task<OperationResult<BlankResource>> HandleRequestAsync(
            OperationParameters<RegisterUserRequest> parameters)
        {
            var checkParams = new UsernameAvailabilityCheckParameters(parameters.Request.Username);
            if (!await _userRepository.UsernameIsAvailableAsync(checkParams))
            {
                return ValidationFailed(nameof(parameters.Request.Username), "Already taken.");
            }
            
            var hashedPassword = _passwordHelper.HashPassword(parameters.Request.Password);

            var createData = _mapper.Map<UserCreateData>(parameters.Request);
            (createData.PasswordHash, createData.PasswordSalt) = hashedPassword;
            
            await _userRepository.CreateUserAsync(createData);

            return Success();
        }
    }
}
