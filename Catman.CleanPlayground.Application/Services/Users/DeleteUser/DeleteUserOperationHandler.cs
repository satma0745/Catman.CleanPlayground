namespace Catman.CleanPlayground.Application.Services.Users.DeleteUser
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Catman.CleanPlayground.Application.Persistence.UnitOfWork;
    using Catman.CleanPlayground.Application.Services.Common.Operation.Handler;
    using Catman.CleanPlayground.Application.Services.Common.Response;
    using Catman.CleanPlayground.Application.Session;
    using FluentValidation;

    internal class DeleteUserOperationHandler : OperationHandlerBase<DeleteUserRequest, BlankResource>
    {
        private readonly IUnitOfWork _work;

        protected override bool RequireAuthorizedUser => true;

        public DeleteUserOperationHandler(
            IEnumerable<IValidator<DeleteUserRequest>> requestValidators,
            ISessionManager sessionManager,
            IUnitOfWork unitOfWork)
            : base(requestValidators, sessionManager)
        {
            _work = unitOfWork;
        }

        protected override async Task<OperationResult<BlankResource>> HandleRequestAsync(DeleteUserRequest request)
        {
            if (request.Id != Session.CurrentUser.Id)
            {
                return AccessViolation("You can only delete your own profile.");
            }
            
            if (!await _work.Users.UserExistsAsync(request.Id))
            {
                return NotFound("User not found.");
            }
            
            var user = await _work.Users.GetUserAsync(request.Id);
            await _work.Users.RemoveUserAsync(user);

            await _work.SaveAsync();
            
            return Success();
        }
    }
}
