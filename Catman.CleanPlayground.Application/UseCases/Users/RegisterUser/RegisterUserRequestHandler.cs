namespace Catman.CleanPlayground.Application.UseCases.Users.RegisterUser
{
    using System.Threading.Tasks;
    using AutoMapper;
    using Catman.CleanPlayground.Application.Helpers.Password;
    using Catman.CleanPlayground.Application.Localization;
    using Catman.CleanPlayground.Application.Persistence.Entities;
    using Catman.CleanPlayground.Application.Persistence.UnitOfWork;
    using Catman.CleanPlayground.Application.UseCases.Common.RequestHandling;
    using Catman.CleanPlayground.Application.UseCases.Common.Response;

    internal class RegisterUserRequestHandler : RequestHandlerBase<RegisterUserRequest, BlankResource>
    {
        private readonly IPasswordHelper _passwordHelper;
        private readonly IUserLocalizer _localizer;
        private readonly IUnitOfWork _work;
        private readonly IMapper _mapper;

        public RegisterUserRequestHandler(
            IUnitOfWork unitOfWork,
            IUserLocalizer localizer,
            IPasswordHelper passwordHelper,
            IMapper mapper)
        {
            _passwordHelper = passwordHelper;
            _localizer = localizer;
            _work = unitOfWork;
            _mapper = mapper;
        }

        protected override async Task<Response<BlankResource>> HandleAsync(RegisterUserRequest request)
        {
            if (!await _work.Users.UsernameIsAvailableAsync(request.Username))
            {
                return ValidationFailed(nameof(request.Username), _localizer.UsernameAlreadyTaken());
            }
            
            var user = _mapper.Map<UserEntity>(request);
            user.Password = _passwordHelper.HashPassword(request.Password);

            await _work.Users.CreateUserAsync(user);
            
            await _work.SaveAsync();

            return Success();
        }
    }
}
