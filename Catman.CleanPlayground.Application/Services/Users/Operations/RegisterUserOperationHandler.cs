namespace Catman.CleanPlayground.Application.Services.Users.Operations
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper;
    using Catman.CleanPlayground.Application.Helpers.Password;
    using Catman.CleanPlayground.Application.Persistence.Entities;
    using Catman.CleanPlayground.Application.Persistence.UnitOfWork;
    using Catman.CleanPlayground.Application.Services.Common.Operation.Handler;
    using Catman.CleanPlayground.Application.Services.Common.Request;
    using Catman.CleanPlayground.Application.Services.Common.Response;
    using Catman.CleanPlayground.Application.Services.Users.Requests;
    using Catman.CleanPlayground.Application.Session;
    using FluentValidation;

    internal class RegisterUserOperationHandler : OperationHandlerBase<RegisterUserRequest, BlankResource>
    {
        private readonly IPasswordHelper _passwordHelper;
        private readonly IUnitOfWork _work;
        private readonly IMapper _mapper;

        public RegisterUserOperationHandler(
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

        protected override async Task<OperationResult<BlankResource>> HandleRequestAsync(
            OperationParameters<RegisterUserRequest> parameters)
        {
            if (!await _work.Users.UsernameIsAvailableAsync(parameters.Request.Username))
            {
                return ValidationFailed(nameof(parameters.Request.Username), "Already taken.");
            }
            
            var user = _mapper.Map<UserEntity>(parameters.Request);
            user.Password = _passwordHelper.HashPassword(parameters.Request.Password);

            await _work.Users.CreateUserAsync(user);
            
            await _work.SaveAsync();

            return Success();
        }
    }
}
