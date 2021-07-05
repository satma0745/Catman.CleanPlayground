namespace Catman.CleanPlayground.Application.Services.Users.Operations
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper;
    using Catman.CleanPlayground.Application.Helpers.Password;
    using Catman.CleanPlayground.Application.Persistence.Entities;
    using Catman.CleanPlayground.Application.Persistence.Repositories;
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
            if (!await _userRepository.UsernameIsAvailableAsync(parameters.Request.Username))
            {
                return ValidationFailed(nameof(parameters.Request.Username), "Already taken.");
            }
            
            var user = _mapper.Map<UserEntity>(parameters.Request);
            user.Password = _passwordHelper.HashPassword(parameters.Request.Password);

            await _userRepository.CreateUserAsync(user);

            return Success();
        }
    }
}
