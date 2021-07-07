namespace Catman.CleanPlayground.Application.UseCases.Users.DeleteUser
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Catman.CleanPlayground.Application.Persistence.UnitOfWork;
    using Catman.CleanPlayground.Application.Session;
    using Catman.CleanPlayground.Application.UseCases.Common.RequestHandler;
    using Catman.CleanPlayground.Application.UseCases.Common.Response;
    using FluentValidation;

    internal class DeleteUserRequestHandler : RequestHandlerBase<DeleteUserRequest, BlankResource>
    {
        private readonly IUnitOfWork _work;

        protected override bool RequireAuthorizedUser => true;

        public DeleteUserRequestHandler(
            IEnumerable<IValidator<DeleteUserRequest>> requestValidators,
            ISessionManager sessionManager,
            IUnitOfWork unitOfWork)
            : base(requestValidators, sessionManager)
        {
            _work = unitOfWork;
        }

        protected override async Task<Response<BlankResource>> HandleAsync(DeleteUserRequest request)
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
