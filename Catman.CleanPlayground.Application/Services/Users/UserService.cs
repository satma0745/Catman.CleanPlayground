namespace Catman.CleanPlayground.Application.Services.Users
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Catman.CleanPlayground.Application.Services.Common.Operation;
    using Catman.CleanPlayground.Application.Services.Common.Response;
    using Catman.CleanPlayground.Application.Services.Users.Requests;
    using Catman.CleanPlayground.Application.Services.Users.Resources;

    internal class UserService : IUserService
    {
        private readonly IOperation<GetUsersRequest, ICollection<UserResource>> _getUsersOperation;
        private readonly IOperation<RegisterUserRequest, BlankResource> _registerUserOperation;
        private readonly IOperation<UpdateUserRequest, BlankResource> _updateUserOperation;
        private readonly IOperation<DeleteUserRequest, BlankResource> _deleteUserOperation;

        public UserService(
            IOperation<GetUsersRequest, ICollection<UserResource>> getUsersOperation,
            IOperation<RegisterUserRequest, BlankResource> registerUserOperation,
            IOperation<UpdateUserRequest, BlankResource> updateUserOperation,
            IOperation<DeleteUserRequest, BlankResource> deleteUserOperation)
        {
            _getUsersOperation = getUsersOperation;
            _registerUserOperation = registerUserOperation;
            _updateUserOperation = updateUserOperation;
            _deleteUserOperation = deleteUserOperation;
        }

        public Task<OperationResult<ICollection<UserResource>>> GetUsersAsync(string authenticationToken) =>
            _getUsersOperation.PerformAsync(new GetUsersRequest(), authenticationToken);

        public Task<OperationResult<BlankResource>> RegisterUserAsync(
            RegisterUserRequest registerRequest,
            string authenticationToken) =>
            _registerUserOperation.PerformAsync(registerRequest, authenticationToken);

        public Task<OperationResult<BlankResource>> UpdateUserAsync(
            UpdateUserRequest updateRequest,
            string authenticationToken) =>
            _updateUserOperation.PerformAsync(updateRequest, authenticationToken);

        public Task<OperationResult<BlankResource>> DeleteUserAsync(
            DeleteUserRequest deleteRequest,
            string authenticationToken) =>
            _deleteUserOperation.PerformAsync(deleteRequest, authenticationToken);
    }
}
