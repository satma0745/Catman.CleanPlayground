namespace Catman.CleanPlayground.Application.UseCases.Users
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Catman.CleanPlayground.Application.UseCases.Common.RequestHandler;
    using Catman.CleanPlayground.Application.UseCases.Common.Response;
    using Catman.CleanPlayground.Application.UseCases.Users.DeleteUser;
    using Catman.CleanPlayground.Application.UseCases.Users.GetUsers;
    using Catman.CleanPlayground.Application.UseCases.Users.RegisterUser;
    using Catman.CleanPlayground.Application.UseCases.Users.UpdateUser;

    internal class UserService : IUserService
    {
        private readonly IRequestHandler<GetUsersRequest, ICollection<UserResource>> _getUsersRequestHandler;
        private readonly IRequestHandler<RegisterUserRequest, BlankResource> _registerUserRequestHandler;
        private readonly IRequestHandler<UpdateUserRequest, BlankResource> _updateUserRequestHandler;
        private readonly IRequestHandler<DeleteUserRequest, BlankResource> _deleteUserRequestHandler;

        public UserService(
            IRequestHandler<GetUsersRequest, ICollection<UserResource>> getUsersRequestHandler,
            IRequestHandler<RegisterUserRequest, BlankResource> registerUserRequestHandler,
            IRequestHandler<UpdateUserRequest, BlankResource> updateUserRequestHandler,
            IRequestHandler<DeleteUserRequest, BlankResource> deleteUserRequestHandler)
        {
            _getUsersRequestHandler = getUsersRequestHandler;
            _registerUserRequestHandler = registerUserRequestHandler;
            _updateUserRequestHandler = updateUserRequestHandler;
            _deleteUserRequestHandler = deleteUserRequestHandler;
        }

        public Task<IResponse<ICollection<UserResource>>> GetUsersAsync() =>
            _getUsersRequestHandler.PerformAsync(new GetUsersRequest());

        public Task<IResponse<BlankResource>> RegisterUserAsync(RegisterUserRequest registerRequest) =>
            _registerUserRequestHandler.PerformAsync(registerRequest);

        public Task<IResponse<BlankResource>> UpdateUserAsync(UpdateUserRequest updateRequest) =>
            _updateUserRequestHandler.PerformAsync(updateRequest);

        public Task<IResponse<BlankResource>> DeleteUserAsync(DeleteUserRequest deleteRequest) =>
            _deleteUserRequestHandler.PerformAsync(deleteRequest);
    }
}
