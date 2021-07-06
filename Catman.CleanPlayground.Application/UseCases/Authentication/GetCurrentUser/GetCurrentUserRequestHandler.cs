namespace Catman.CleanPlayground.Application.UseCases.Authentication.GetCurrentUser
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper;
    using Catman.CleanPlayground.Application.Persistence.UnitOfWork;
    using Catman.CleanPlayground.Application.Session;
    using Catman.CleanPlayground.Application.UseCases.Common.RequestHandler.Handler;
    using Catman.CleanPlayground.Application.UseCases.Common.Response;
    using FluentValidation;

    internal class GetCurrentUserRequestHandler : RequestHandlerBase<GetCurrentUserRequest, CurrentUserResource>
    {
        private readonly IUnitOfWork _work;
        private readonly IMapper _mapper;
        
        protected override bool RequireAuthorizedUser => true;

        public GetCurrentUserRequestHandler(
            IEnumerable<IValidator<GetCurrentUserRequest>> requestValidators,
            ISessionManager sessionManager,
            IUnitOfWork unitOfWork,
            IMapper mapper)
            : base(requestValidators, sessionManager)
        {
            _work = unitOfWork;
            _mapper = mapper;
        }

        protected override async Task<OperationResult<CurrentUserResource>> HandleRequestAsync(GetCurrentUserRequest _)
        {
            var currentUserId = Session.CurrentUser.Id;
            var currentUser = await _work.Users.GetUserAsync(currentUserId);

            return Success(_mapper.Map<CurrentUserResource>(currentUser));
        }
    }
}
