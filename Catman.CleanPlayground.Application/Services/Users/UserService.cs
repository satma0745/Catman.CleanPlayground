namespace Catman.CleanPlayground.Application.Services.Users
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Catman.CleanPlayground.Application.Services.Common.Response;
    using Catman.CleanPlayground.Application.Services.Users.Operations;
    using Catman.CleanPlayground.Application.Services.Users.Requests;
    using Catman.CleanPlayground.Application.Services.Users.Resources;

    internal class UserService : IUserService
    {
        private readonly GetUsersOperationHandler _getUsersOperationHandler;
        private readonly RegisterUserOperationHandler _registerUserOperationHandler;
        private readonly UpdateUserOperationHandler _updateUserOperationHandler;
        private readonly DeleteUserOperationHandler _deleteUserOperationHandler;

        public UserService(
            GetUsersOperationHandler getUsersOperationHandler,
            RegisterUserOperationHandler registerUserOperationHandler,
            UpdateUserOperationHandler updateUserOperationHandler,
            DeleteUserOperationHandler deleteUserOperationHandler)
        {
            _getUsersOperationHandler = getUsersOperationHandler;
            _registerUserOperationHandler = registerUserOperationHandler;
            _updateUserOperationHandler = updateUserOperationHandler;
            _deleteUserOperationHandler = deleteUserOperationHandler;
        }

        public Task<OperationResult<ICollection<UserResource>>> GetUsersAsync() =>
            _getUsersOperationHandler.HandleAsync();

        public Task<OperationResult<OperationSuccess>> RegisterUserAsync(RegisterUserRequest registerRequest) =>
            _registerUserOperationHandler.HandleAsync(registerRequest);

        public Task<OperationResult<OperationSuccess>> UpdateUserAsync(UpdateUserRequest updateRequest) =>
            _updateUserOperationHandler.HandleAsync(updateRequest);

        public Task<OperationResult<OperationSuccess>> DeleteUserAsync(DeleteUserRequest deleteRequest) =>
            _deleteUserOperationHandler.HandleAsync(deleteRequest);
    }
}
