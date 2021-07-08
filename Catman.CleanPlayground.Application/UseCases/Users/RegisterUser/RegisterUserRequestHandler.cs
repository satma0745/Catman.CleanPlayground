namespace Catman.CleanPlayground.Application.UseCases.Users.RegisterUser
{
    using System.Threading.Tasks;
    using AutoMapper;
    using Catman.CleanPlayground.Application.Helpers.AuthorizationToken;
    using Catman.CleanPlayground.Application.Helpers.Password;
    using Catman.CleanPlayground.Application.Helpers.Session;
    using Catman.CleanPlayground.Application.Persistence.Entities;
    using Catman.CleanPlayground.Application.Persistence.UnitOfWork;
    using Catman.CleanPlayground.Application.UseCases.Common.RequestHandling;
    using Catman.CleanPlayground.Application.UseCases.Common.Response;

    internal class RegisterUserRequestHandler : RequestHandlerBase<RegisterUserRequest, BlankResource>
    {
        private readonly IPasswordHelper _passwordHelper;
        private readonly IUnitOfWork _work;
        private readonly IMapper _mapper;

        public RegisterUserRequestHandler(
            ISessionManager sessionManager,
            ITokenHelper tokenHelper,
            IUnitOfWork unitOfWork,
            IPasswordHelper passwordHelper,
            IMapper mapper)
            : base(sessionManager, tokenHelper)
        {
            _passwordHelper = passwordHelper;
            _work = unitOfWork;
            _mapper = mapper;
        }

        protected override async Task<Response<BlankResource>> HandleAsync(RegisterUserRequest request)
        {
            if (!await _work.Users.UsernameIsAvailableAsync(request.Username))
            {
                return ValidationFailed(nameof(request.Username), "Already taken.");
            }
            
            var user = _mapper.Map<UserEntity>(request);
            user.Password = _passwordHelper.HashPassword(request.Password);

            await _work.Users.CreateUserAsync(user);
            
            await _work.SaveAsync();

            return Success();
        }
    }
}
