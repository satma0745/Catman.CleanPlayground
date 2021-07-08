namespace Catman.CleanPlayground.Application.UseCases.Authentication.GetCurrentUser
{
    using System.Threading.Tasks;
    using AutoMapper;
    using Catman.CleanPlayground.Application.Helpers.AuthorizationToken;
    using Catman.CleanPlayground.Application.Helpers.Session;
    using Catman.CleanPlayground.Application.Persistence.UnitOfWork;
    using Catman.CleanPlayground.Application.UseCases.Common.RequestHandling;
    using Catman.CleanPlayground.Application.UseCases.Common.Response;

    internal class GetCurrentUserRequestHandler : RequestHandlerBase<GetCurrentUserRequest, CurrentUserResource>
    {
        private readonly ISessionManager _sessionManager;
        private readonly IUnitOfWork _work;
        private readonly IMapper _mapper;
        
        protected override bool RequireAuthorizedUser => true;

        public GetCurrentUserRequestHandler(
            ISessionManager sessionManager,
            ITokenHelper tokenHelper,
            IUnitOfWork unitOfWork,
            IMapper mapper)
            : base(sessionManager, tokenHelper)
        {
            _sessionManager = sessionManager;
            _work = unitOfWork;
            _mapper = mapper;
        }

        protected override async Task<Response<CurrentUserResource>> HandleAsync(GetCurrentUserRequest _)
        {
            var currentUserId = _sessionManager.CurrentUser.Id;
            var currentUser = await _work.Users.GetUserAsync(currentUserId);

            return Success(_mapper.Map<CurrentUserResource>(currentUser));
        }
    }
}
