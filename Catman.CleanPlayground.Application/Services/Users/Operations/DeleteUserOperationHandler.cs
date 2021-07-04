namespace Catman.CleanPlayground.Application.Services.Users.Operations
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Catman.CleanPlayground.Application.Persistence.Users;
    using Catman.CleanPlayground.Application.Services.Common.Operation;
    using Catman.CleanPlayground.Application.Services.Common.Request;
    using Catman.CleanPlayground.Application.Services.Common.Response;
    using Catman.CleanPlayground.Application.Services.Common.Response.Errors;
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
                var accessViolationError = new AccessViolationError("You can only delete your own profile.");
                return new OperationResult<BlankResource>(accessViolationError);
            }
                
            if (!await _userRepository.UserExistsAsync(parameters.Request.Id))
            {
                var notFoundError = new NotFoundError("User not found.");
                return new OperationResult<BlankResource>(notFoundError);
            }
            
            await _userRepository.RemoveUserAsync(parameters.Request.Id);
                
            return new OperationResult<BlankResource>(new BlankResource());
        }
    }
}
