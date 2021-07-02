namespace Catman.CleanPlayground.Application.Services.Users
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Catman.CleanPlayground.Application.Services.Common.Response;
    using Catman.CleanPlayground.Application.Services.Users.Models;
    using Catman.CleanPlayground.Application.Services.Users.Operations;

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

        public Task<OperationResult<ICollection<UserModel>>> GetUsersAsync() =>
            _getUsersOperationHandler.HandleAsync();

        public Task<OperationResult> RegisterUserAsync(RegisterUserModel registerModel) =>
            _registerUserOperationHandler.HandleAsync(registerModel);

        public Task<OperationResult> UpdateUserAsync(UpdateUserModel updateModel) =>
            _updateUserOperationHandler.HandleAsync(updateModel);

        public Task<OperationResult> DeleteUserAsync(byte userId) =>
            _deleteUserOperationHandler.HandleAsync(userId);
    }
}
