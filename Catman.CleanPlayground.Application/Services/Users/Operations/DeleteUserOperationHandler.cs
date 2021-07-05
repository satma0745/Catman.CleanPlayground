namespace Catman.CleanPlayground.Application.Services.Users.Operations
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Catman.CleanPlayground.Application.Persistence.Repositories;
    using Catman.CleanPlayground.Application.Services.Common.Operation.Handler;
    using Catman.CleanPlayground.Application.Services.Common.Request;
    using Catman.CleanPlayground.Application.Services.Common.Response;
    using Catman.CleanPlayground.Application.Services.Users.Requests;
    using Catman.CleanPlayground.Application.Session;
    using FluentValidation;

    internal class DeleteUserOperationHandler : OperationHandlerBase<DeleteUserRequest, BlankResource>
    {
        private readonly IUserRepository _userRepository;

        protected override bool RequireAuthorizedUser => true;

        public DeleteUserOperationHandler(
            IUserRepository userRepository,
            IEnumerable<IValidator<DeleteUserRequest>> requestValidators,
            ISessionManager sessionManager)
            : base(requestValidators, sessionManager)
        {
            _userRepository = userRepository;
        }

        protected override async Task<OperationResult<BlankResource>> HandleRequestAsync(
            OperationParameters<DeleteUserRequest> parameters)
        {
            if (parameters.Request.Id != parameters.Session.CurrentUser.Id)
            {
                return AccessViolation("You can only delete your own profile.");
            }
            
            if (!await _userRepository.UserExistsAsync(parameters.Request.Id))
            {
                return NotFound("User not found.");
            }
            
            await _userRepository.RemoveUserAsync(parameters.Request.Id);
            return Success();
        }
    }
}
