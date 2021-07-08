namespace Catman.CleanPlayground.Application.UseCases.Users.GetUsers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper;
    using Catman.CleanPlayground.Application.Helpers.AuthorizationToken;
    using Catman.CleanPlayground.Application.Helpers.Session;
    using Catman.CleanPlayground.Application.Persistence.UnitOfWork;
    using Catman.CleanPlayground.Application.UseCases.Common.RequestHandling;
    using Catman.CleanPlayground.Application.UseCases.Common.Response;

    internal class GetUsersRequestHandler : RequestHandlerBase<GetUsersRequest, ICollection<UserResource>>
    {
        private readonly IUnitOfWork _work;
        private readonly IMapper _mapper;

        public GetUsersRequestHandler(
            ISessionManager sessionManager,
            ITokenHelper tokenHelper,
            IUnitOfWork unitOfWork,
            IMapper mapper)
            : base(sessionManager, tokenHelper)
        {
            _mapper = mapper;
            _work = unitOfWork;
        }

        protected override async Task<Response<ICollection<UserResource>>> HandleAsync(GetUsersRequest _)
        {
            var userEntities = await _work.Users.GetUsersAsync();
            
            var users = _mapper.Map<ICollection<UserResource>>(userEntities);
            
            return Success(users);
        }
    }
}
