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

    internal class UpdateUserOperationHandler : OperationHandlerBase<UpdateUserRequest, BlankResource>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        protected override bool RequireAuthorizedUser => true;

        public UpdateUserOperationHandler(
            IUserRepository userRepository,
            IEnumerable<IValidator<UpdateUserRequest>> requestValidators,
            ITokenManager tokenManager,
            IMapper mapper)
            : base(requestValidators, userRepository, tokenManager, mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        
        protected override async Task<OperationResult<BlankResource>> HandleRequestAsync(
            OperationParameters<UpdateUserRequest> parameters)
        {
            if (parameters.Request.Id != parameters.CurrentUser.Id)
            {
                var accessViolationError = new AccessViolationError("You can only edit your own profile.");
                return new OperationResult<BlankResource>(accessViolationError);
            }
                
            if (!await _userRepository.UserExistsAsync(parameters.Request.Id))
            {
                var notFoundError = new NotFoundError("User not found.");
                return new OperationResult<BlankResource>(notFoundError);
            }

            var usernameAvailabilityCheckParameters =
                new UsernameAvailabilityCheckParameters(parameters.Request.Username, parameters.Request.Id);
            if (!await _userRepository.UsernameIsAvailableAsync(usernameAvailabilityCheckParameters))
            {
                var validationMessages = new Dictionary<string, string>
                {
                    {nameof(parameters.Request.Username), "Already taken."}
                };
                var validationError = new ValidationError(validationMessages);
                return new OperationResult<BlankResource>(validationError);
            }

            var updateData = _mapper.Map<UserUpdateData>(parameters.Request);
            await _userRepository.UpdateUserAsync(updateData);

            return new OperationResult<BlankResource>(new BlankResource());
        }
    }
}
