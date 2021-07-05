namespace Catman.CleanPlayground.Application.Services.Authentication.Operations
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper;
    using Catman.CleanPlayground.Application.Persistence.UnitOfWork;
    using Catman.CleanPlayground.Application.Services.Authentication.Requests;
    using Catman.CleanPlayground.Application.Services.Authentication.Resources;
    using Catman.CleanPlayground.Application.Services.Common.Operation.Handler;
    using Catman.CleanPlayground.Application.Services.Common.Request;
    using Catman.CleanPlayground.Application.Services.Common.Response;
    using Catman.CleanPlayground.Application.Session;
    using FluentValidation;

    internal class GetCurrentUserOperationHandler : OperationHandlerBase<GetCurrentUserRequest, CurrentUserResource>
    {
        private readonly IUnitOfWork _work;
        private readonly IMapper _mapper;
        
        protected override bool RequireAuthorizedUser => true;

        public GetCurrentUserOperationHandler(
            IEnumerable<IValidator<GetCurrentUserRequest>> requestValidators,
            ISessionManager sessionManager,
            IUnitOfWork unitOfWork,
            IMapper mapper)
            : base(requestValidators, sessionManager)
        {
            _work = unitOfWork;
            _mapper = mapper;
        }

        protected override async Task<OperationResult<CurrentUserResource>> HandleRequestAsync(
            OperationParameters<GetCurrentUserRequest> parameters)
        {
            var currentUserId = parameters.Session.CurrentUser.Id;
            var currentUser = await _work.Users.GetUserAsync(currentUserId);

            return Success(_mapper.Map<CurrentUserResource>(currentUser));
        }
    }
}