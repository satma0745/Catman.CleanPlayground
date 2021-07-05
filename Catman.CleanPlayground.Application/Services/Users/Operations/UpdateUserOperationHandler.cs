namespace Catman.CleanPlayground.Application.Services.Users.Operations
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper;
    using Catman.CleanPlayground.Application.Helpers.Password;
    using Catman.CleanPlayground.Application.Persistence.UnitOfWork;
    using Catman.CleanPlayground.Application.Services.Common.Operation.Handler;
    using Catman.CleanPlayground.Application.Services.Common.Request;
    using Catman.CleanPlayground.Application.Services.Common.Response;
    using Catman.CleanPlayground.Application.Services.Users.Requests;
    using Catman.CleanPlayground.Application.Session;
    using FluentValidation;

    internal class UpdateUserOperationHandler : OperationHandlerBase<UpdateUserRequest, BlankResource>
    {
        private readonly IPasswordHelper _passwordHelper;
        private readonly IUnitOfWork _work;
        private readonly IMapper _mapper;

        protected override bool RequireAuthorizedUser => true;

        public UpdateUserOperationHandler(
            IEnumerable<IValidator<UpdateUserRequest>> requestValidators,
            IPasswordHelper passwordHelper,
            ISessionManager sessionManager,
            IUnitOfWork unitOfWork,
            IMapper mapper)
            : base(requestValidators, sessionManager)
        {
            _passwordHelper = passwordHelper;
            _mapper = mapper;
            _work = unitOfWork;
        }
        
        protected override async Task<OperationResult<BlankResource>> HandleRequestAsync(
            OperationParameters<UpdateUserRequest> parameters)
        {
            if (parameters.Request.Id != parameters.Session.CurrentUser.Id)
            {
                return AccessViolation("You can only edit your own profile.");
            }
                
            if (!await _work.Users.UserExistsAsync(parameters.Request.Id))
            {
                return NotFound("User not found.");
            }

            if (!await _work.Users.UsernameIsAvailableAsync(parameters.Request.Username, parameters.Request.Id))
            {
                return ValidationFailed(nameof(parameters.Request.Username), "Already taken.");
            }

            var user = await _work.Users.GetUserAsync(parameters.Request.Id);

            _mapper.Map(parameters.Request, user);
            user.Password = _passwordHelper.HashPassword(parameters.Request.Password);
            
            await _work.SaveAsync();

            return Success();
        }
    }
}
