namespace Catman.CleanPlayground.Application.UseCases.Users.UpdateUser
{
    using System.Threading.Tasks;
    using AutoMapper;
    using Catman.CleanPlayground.Application.Helpers.Password;
    using Catman.CleanPlayground.Application.Helpers.Session;
    using Catman.CleanPlayground.Application.Localization;
    using Catman.CleanPlayground.Application.Persistence.UnitOfWork;
    using Catman.CleanPlayground.Application.UseCases.Common.RequestHandling;
    using Catman.CleanPlayground.Application.UseCases.Common.Response;

    internal class UpdateUserRequestHandler : RequestHandlerBase<UpdateUserRequest, BlankResource>
    {
        private readonly ISessionManager _sessionManager;
        private readonly IPasswordHelper _passwordHelper;
        private readonly IUserLocalizer _localizer;
        private readonly IUnitOfWork _work;
        private readonly IMapper _mapper;

        public UpdateUserRequestHandler(
            ISessionManager sessionManager,
            IPasswordHelper passwordHelper,
            IUserLocalizer localizer,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _sessionManager = sessionManager;
            _passwordHelper = passwordHelper;
            _localizer = localizer;
            _mapper = mapper;
            _work = unitOfWork;
        }
        
        protected override async Task<Response<BlankResource>> HandleAsync(UpdateUserRequest request)
        {
            if (request.Id != _sessionManager.CurrentUser.Id)
            {
                return AccessViolation(_localizer.AttemptToEditAnotherUser());
            }
                
            if (!await _work.Users.UserExistsAsync(request.Id))
            {
                return NotFound(_localizer.NotFound());
            }

            if (!await _work.Users.UsernameIsAvailableAsync(request.Username, request.Id))
            {
                return ValidationFailed(nameof(request.Username), _localizer.UsernameAlreadyTaken());
            }

            var user = await _work.Users.GetUserAsync(request.Id);

            _mapper.Map(request, user);
            user.Password = _passwordHelper.HashPassword(request.Password);
            
            await _work.SaveAsync();

            return Success();
        }
    }
}
