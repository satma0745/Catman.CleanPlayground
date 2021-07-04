namespace Catman.CleanPlayground.Application.Services.Users.Operations
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper;
    using Catman.CleanPlayground.Application.Authentication;
    using Catman.CleanPlayground.Application.Persistence.Users;
    using Catman.CleanPlayground.Application.Services.Common.Operation;
    using Catman.CleanPlayground.Application.Services.Common.Request;
    using Catman.CleanPlayground.Application.Services.Common.Response;
    using Catman.CleanPlayground.Application.Services.Users.Requests;
    using Catman.CleanPlayground.Application.Services.Users.Resources;
    using FluentValidation;

    internal class GetUsersOperationHandler : OperationHandlerBase<GetUsersRequest, ICollection<UserResource>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetUsersOperationHandler(
            IUserRepository userRepository,
            IEnumerable<IValidator<GetUsersRequest>> requestValidators,
            ITokenManager tokenManager,
            IMapper mapper)
            : base(requestValidators, userRepository, tokenManager, mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        protected override async Task<OperationResult<ICollection<UserResource>>> HandleRequestAsync(
            OperationParameters<GetUsersRequest> _)
        {
            var usersData = await _userRepository.GetUsersAsync();
            var users = _mapper.Map<ICollection<UserResource>>(usersData);
            
            return new OperationResult<ICollection<UserResource>>(users);
        }
    }
}
