namespace Catman.CleanPlayground.Application.UseCases.Users.UpdateUser
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper;
    using Catman.CleanPlayground.Application.Helpers.Password;
    using Catman.CleanPlayground.Application.Persistence.UnitOfWork;
    using Catman.CleanPlayground.Application.Session;
    using Catman.CleanPlayground.Application.UseCases.Common.RequestHandler.Handler;
    using Catman.CleanPlayground.Application.UseCases.Common.Response;
    using FluentValidation;

    internal class UpdateUserRequestHandler : RequestHandlerBase<UpdateUserRequest, BlankResource>
    {
        private readonly IPasswordHelper _passwordHelper;
        private readonly IUnitOfWork _work;
        private readonly IMapper _mapper;

        protected override bool RequireAuthorizedUser => true;

        public UpdateUserRequestHandler(
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
        
        protected override async Task<OperationResult<BlankResource>> HandleRequestAsync(UpdateUserRequest request)
        {
            if (request.Id != Session.CurrentUser.Id)
            {
                return AccessViolation("You can only edit your own profile.");
            }
                
            if (!await _work.Users.UserExistsAsync(request.Id))
            {
                return NotFound("User not found.");
            }

            if (!await _work.Users.UsernameIsAvailableAsync(request.Username, request.Id))
            {
                return ValidationFailed(nameof(request.Username), "Already taken.");
            }

            var user = await _work.Users.GetUserAsync(request.Id);

            _mapper.Map(request, user);
            user.Password = _passwordHelper.HashPassword(request.Password);
            
            await _work.SaveAsync();

            return Success();
        }
    }
}
