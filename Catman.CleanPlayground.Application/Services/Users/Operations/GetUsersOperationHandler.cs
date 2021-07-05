namespace Catman.CleanPlayground.Application.Services.Users.Operations
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper;
    using Catman.CleanPlayground.Application.Persistence.Repositories;
    using Catman.CleanPlayground.Application.Services.Common.Operation.Handler;
    using Catman.CleanPlayground.Application.Services.Common.Request;
    using Catman.CleanPlayground.Application.Services.Common.Response;
    using Catman.CleanPlayground.Application.Services.Users.Requests;
    using Catman.CleanPlayground.Application.Services.Users.Resources;
    using Catman.CleanPlayground.Application.Session;
    using FluentValidation;

    internal class GetUsersOperationHandler : OperationHandlerBase<GetUsersRequest, ICollection<UserResource>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetUsersOperationHandler(
            IUserRepository userRepository,
            IEnumerable<IValidator<GetUsersRequest>> requestValidators,
            ISessionManager sessionManager,
            IMapper mapper)
            : base(requestValidators, sessionManager)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        protected override async Task<OperationResult<ICollection<UserResource>>> HandleRequestAsync(
            OperationParameters<GetUsersRequest> _)
        {
            var userEntities = await _userRepository.GetUsersAsync();
            var users = _mapper.Map<ICollection<UserResource>>(userEntities);
            return Success(users);
        }
    }
}
