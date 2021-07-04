namespace Catman.CleanPlayground.Application.Services.Users.Operations
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper;
    using Catman.CleanPlayground.Application.Authentication;
    using Catman.CleanPlayground.Application.Persistence.Users;
    using Catman.CleanPlayground.Application.Services.Common.Operation;
    using Catman.CleanPlayground.Application.Services.Common.Request;
    using Catman.CleanPlayground.Application.Services.Common.Response;
    using Catman.CleanPlayground.Application.Services.Common.Response.Errors;
    using Catman.CleanPlayground.Application.Services.Users.Requests;
    using FluentValidation;

    internal class RegisterUserOperationHandler : OperationHandlerBase<RegisterUserRequest, BlankResource>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public RegisterUserOperationHandler(
            IEnumerable<IValidator<RegisterUserRequest>> requestValidators,
            IUserRepository userRepository,
            ITokenManager tokenManager,
            IMapper mapper)
            : base(requestValidators, userRepository, tokenManager, mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        protected override async Task<OperationResult<BlankResource>> HandleRequestAsync(
            OperationParameters<RegisterUserRequest> parameters)
        {
            var checkParams = new UsernameAvailabilityCheckParameters(parameters.Request.Username);
            if (!await _userRepository.UsernameIsAvailableAsync(checkParams))
            {
                var validationMessages = new Dictionary<string, string>
                {
                    {nameof(parameters.Request.Username), "Already taken."}
                };
                var validationError = new ValidationError(validationMessages);
                return new OperationResult<BlankResource>(validationError);
            }

            var createData = _mapper.Map<UserCreateData>(parameters.Request);
            await _userRepository.CreateUserAsync(createData);

            return new OperationResult<BlankResource>(new BlankResource());
        }
    }
}
