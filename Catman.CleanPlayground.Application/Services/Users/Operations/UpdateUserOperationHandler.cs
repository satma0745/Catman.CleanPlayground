namespace Catman.CleanPlayground.Application.Services.Users.Operations
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper;
    using Catman.CleanPlayground.Application.Persistence.Users;
    using Catman.CleanPlayground.Application.Services.Common.Operation.Handler;
    using Catman.CleanPlayground.Application.Services.Common.Request;
    using Catman.CleanPlayground.Application.Services.Common.Response;
    using Catman.CleanPlayground.Application.Services.Users.Requests;
    using Catman.CleanPlayground.Application.Session;
    using FluentValidation;

    internal class UpdateUserOperationHandler : OperationHandlerBase<UpdateUserRequest, BlankResource>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        protected override bool RequireAuthorizedUser => true;

        public UpdateUserOperationHandler(
            IUserRepository userRepository,
            IEnumerable<IValidator<UpdateUserRequest>> requestValidators,
            ISessionManager sessionManager,
            IMapper mapper)
            : base(requestValidators, sessionManager)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        
        protected override async Task<OperationResult<BlankResource>> HandleRequestAsync(
            OperationParameters<UpdateUserRequest> parameters)
        {
            if (parameters.Request.Id != parameters.Session.CurrentUser.Id)
            {
                return AccessViolation("You can only edit your own profile.");
            }
                
            if (!await _userRepository.UserExistsAsync(parameters.Request.Id))
            {
                return NotFound("User not found.");
            }

            var usernameAvailabilityCheckParameters =
                new UsernameAvailabilityCheckParameters(parameters.Request.Username, parameters.Request.Id);
            if (!await _userRepository.UsernameIsAvailableAsync(usernameAvailabilityCheckParameters))
            {
                return ValidationFailed(nameof(parameters.Request.Username), "Already taken.");
            }

            var updateData = _mapper.Map<UserUpdateData>(parameters.Request);
            await _userRepository.UpdateUserAsync(updateData);

            return Success();
        }
    }
}
