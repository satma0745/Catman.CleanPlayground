namespace Catman.CleanPlayground.Application.UseCases.Users.DeleteUser
{
    using System.Threading.Tasks;
    using Catman.CleanPlayground.Application.Helpers.Session;
    using Catman.CleanPlayground.Application.Localization;
    using Catman.CleanPlayground.Application.Persistence.UnitOfWork;
    using Catman.CleanPlayground.Application.UseCases.Common.RequestHandling;
    using Catman.CleanPlayground.Application.UseCases.Common.Response;

    internal class DeleteUserRequestHandler : RequestHandlerBase<DeleteUserRequest, BlankResource>
    {
        private readonly ISessionManager _sessionManager;
        private readonly IUserLocalizer _localizer;
        private readonly IUnitOfWork _work;

        public DeleteUserRequestHandler(
            ISessionManager sessionManager,
            IUserLocalizer localizer,
            IUnitOfWork unitOfWork)
        {
            _sessionManager = sessionManager;
            _localizer = localizer;
            _work = unitOfWork;
        }

        protected override async Task<Response<BlankResource>> HandleAsync(DeleteUserRequest request)
        {
            if (request.Id != _sessionManager.CurrentUser.Id)
            {
                return AccessViolation(_localizer.AttemptToDeleteAnotherUser());
            }
            
            if (!await _work.Users.UserExistsAsync(request.Id))
            {
                return AccessViolation(_localizer.NotFound());
            }
            
            var user = await _work.Users.GetUserAsync(request.Id);
            await _work.Users.RemoveUserAsync(user);

            await _work.SaveAsync();
            
            return Success();
        }
    }
}
