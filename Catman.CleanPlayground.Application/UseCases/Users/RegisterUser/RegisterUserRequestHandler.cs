namespace Catman.CleanPlayground.Application.UseCases.Users.RegisterUser
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper;
    using Catman.CleanPlayground.Application.Helpers.Password;
    using Catman.CleanPlayground.Application.Persistence.Entities;
    using Catman.CleanPlayground.Application.Persistence.UnitOfWork;
    using Catman.CleanPlayground.Application.Session;
    using Catman.CleanPlayground.Application.UseCases.Common.RequestHandler.Handler;
    using Catman.CleanPlayground.Application.UseCases.Common.Response;
    using FluentValidation;

    internal class RegisterUserRequestHandler : RequestHandlerBase<RegisterUserRequest, BlankResource>
    {
        private readonly IPasswordHelper _passwordHelper;
        private readonly IUnitOfWork _work;
        private readonly IMapper _mapper;

        public RegisterUserRequestHandler(
            IEnumerable<IValidator<RegisterUserRequest>> requestValidators,
            ISessionManager sessionManager,
            IUnitOfWork unitOfWork,
            IPasswordHelper passwordHelper,
            IMapper mapper)
            : base(requestValidators, sessionManager)
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
