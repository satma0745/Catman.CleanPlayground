namespace Catman.CleanPlayground.Application.UseCases.Users.GetUsers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper;
    using Catman.CleanPlayground.Application.Persistence.UnitOfWork;
    using Catman.CleanPlayground.Application.Session;
    using Catman.CleanPlayground.Application.UseCases.Common.RequestHandler.Handler;
    using Catman.CleanPlayground.Application.UseCases.Common.Response;
    using FluentValidation;

    internal class GetUsersRequestHandler : RequestHandlerBase<GetUsersRequest, ICollection<UserResource>>
    {
        private readonly IUnitOfWork _work;
        private readonly IMapper _mapper;

        public GetUsersRequestHandler(
            IEnumerable<IValidator<GetUsersRequest>> requestValidators,
            ISessionManager sessionManager,
            IUnitOfWork unitOfWork,
            IMapper mapper)
            : base(requestValidators, sessionManager)
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
