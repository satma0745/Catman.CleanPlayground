namespace Catman.CleanPlayground.Application.Helpers.Session
{
    using System;
    using System.Threading.Tasks;
    using AutoMapper;
    using Catman.CleanPlayground.Application.Persistence.UnitOfWork;

    internal class SessionManager : ISessionManager
    {
        public bool Authorized { get; private set; }
        
        public IApplicationUser CurrentUser { get; private set; }
        
        private readonly IUnitOfWork _work;
        private readonly IMapper _mapper;

        public SessionManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _work = unitOfWork;
            _mapper = mapper;
        }
        
        public async Task AuthorizeUserAsync(Guid userId)
        {
            Authorized = true;
            
            var currentUser = await _work.Users.GetUserAsync(userId);
            CurrentUser = _mapper.Map<ApplicationUser>(currentUser);
        }
    }
}
