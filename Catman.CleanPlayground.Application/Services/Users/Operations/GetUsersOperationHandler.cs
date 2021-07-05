namespace Catman.CleanPlayground.Application.Services.Users.Operations
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper;
    using Catman.CleanPlayground.Application.Persistence.UnitOfWork;
    using Catman.CleanPlayground.Application.Services.Common.Operation.Handler;
    using Catman.CleanPlayground.Application.Services.Common.Request;
    using Catman.CleanPlayground.Application.Services.Common.Response;
    using Catman.CleanPlayground.Application.Services.Users.Requests;
    using Catman.CleanPlayground.Application.Services.Users.Resources;
    using Catman.CleanPlayground.Application.Session;
    using FluentValidation;

    internal class GetUsersOperationHandler : OperationHandlerBase<GetUsersRequest, ICollection<UserResource>>
    {
        private readonly IUnitOfWork _work;
        private readonly IMapper _mapper;

        public GetUsersOperationHandler(
            IEnumerable<IValidator<GetUsersRequest>> requestValidators,
            ISessionManager sessionManager,
            IUnitOfWork unitOfWork,
            IMapper mapper)
            : base(requestValidators, sessionManager)
        {
            _mapper = mapper;
            _work = unitOfWork;
        }

        protected override async Task<OperationResult<ICollection<UserResource>>> HandleRequestAsync(
            OperationParameters<GetUsersRequest> _)
        {
            var userEntities = await _work.Users.GetUsersAsync();
            
            var users = _mapper.Map<ICollection<UserResource>>(userEntities);
            
            return Success(users);
        }
    }
}
