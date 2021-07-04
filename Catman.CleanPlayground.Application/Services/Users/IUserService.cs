namespace Catman.CleanPlayground.Application.Services.Users
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Catman.CleanPlayground.Application.Services.Common.Response;
    using Catman.CleanPlayground.Application.Services.Users.Requests;
    using Catman.CleanPlayground.Application.Services.Users.Resources;

    public interface IUserService
    {
        Task<OperationResult<ICollection<UserResource>>> GetUsersAsync(string authenticationToken = default);

        Task<OperationResult<BlankResource>> RegisterUserAsync(
            RegisterUserRequest registerRequest,
            string authenticationToken = default);

        Task<OperationResult<BlankResource>> UpdateUserAsync(
            UpdateUserRequest updateRequest,
            string authenticationToken);

        Task<OperationResult<BlankResource>> DeleteUserAsync(
            DeleteUserRequest deleteRequest,
            string authenticationToken);
    }
}
