namespace Catman.CleanPlayground.Application.UseCases.Users.UpdateUser
{
    using System.Threading.Tasks;
    using AutoMapper;
    using Catman.CleanPlayground.Application.Helpers.AuthorizationToken;
    using Catman.CleanPlayground.Application.Helpers.Password;
    using Catman.CleanPlayground.Application.Helpers.Session;
    using Catman.CleanPlayground.Application.Persistence.UnitOfWork;
    using Catman.CleanPlayground.Application.UseCases.Common.RequestHandling;
    using Catman.CleanPlayground.Application.UseCases.Common.Response;

    internal class UpdateUserRequestHandler : RequestHandlerBase<UpdateUserRequest, BlankResource>
    {
        private readonly ISessionManager _sessionManager;
        private readonly IPasswordHelper _passwordHelper;
        private readonly IUnitOfWork _work;
        private readonly IMapper _mapper;

        protected override bool RequireAuthorizedUser => true;

        public UpdateUserRequestHandler(
            ISessionManager sessionManager,
            ITokenHelper tokenHelper,
            IPasswordHelper passwordHelper,
            IUnitOfWork unitOfWork,
            IMapper mapper)
            : base(sessionManager, tokenHelper)
        {
            _sessionManager = sessionManager;
            _passwordHelper = passwordHelper;
            _mapper = mapper;
            _work = unitOfWork;
        }
        
        protected override async Task<Response<BlankResource>> HandleAsync(UpdateUserRequest request)
        {
            if (request.Id != _sessionManager.CurrentUser.Id)
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
