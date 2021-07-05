namespace Catman.CleanPlayground.Application.Services.Users.Operations
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper;
    using Catman.CleanPlayground.Application.Helpers.Password;
    using Catman.CleanPlayground.Application.Persistence.Repositories;
    using Catman.CleanPlayground.Application.Services.Common.Operation.Handler;
    using Catman.CleanPlayground.Application.Services.Common.Request;
    using Catman.CleanPlayground.Application.Services.Common.Response;
    using Catman.CleanPlayground.Application.Services.Users.Requests;
    using Catman.CleanPlayground.Application.Session;
    using FluentValidation;

    internal class UpdateUserOperationHandler : OperationHandlerBase<UpdateUserRequest, BlankResource>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHelper _passwordHelper;
        private readonly IMapper _mapper;

        protected override bool RequireAuthorizedUser => true;

        public UpdateUserOperationHandler(
            IUserRepository userRepository,
            IEnumerable<IValidator<UpdateUserRequest>> requestValidators,
            IPasswordHelper passwordHelper,
            ISessionManager sessionManager,
            IMapper mapper)
            : base(requestValidators, sessionManager)
        {
            _userRepository = userRepository;
            _passwordHelper = passwordHelper;
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

            if (!await _userRepository.UsernameIsAvailableAsync(parameters.Request.Username, parameters.Request.Id))
            {
                return ValidationFailed(nameof(parameters.Request.Username), "Already taken.");
            }

            var user = await _userRepository.GetUserAsync(parameters.Request.Id);

            _mapper.Map(parameters.Request, user);
            user.Password = _passwordHelper.HashPassword(parameters.Request.Password);
            
            await _userRepository.UpdateUserAsync(user);

            return Success();
        }
    }
}
