namespace Catman.CleanPlayground.Application.Services.Users
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Catman.CleanPlayground.Application.Services.Common.Response;
    using Catman.CleanPlayground.Application.Services.Users.Requests;
    using Catman.CleanPlayground.Application.Services.Users.Resources;

    public interface IUserService
    {
        Task<OperationResult<ICollection<UserResource>>> GetUsersAsync();

        Task<OperationResult<BlankResource>> RegisterUserAsync(RegisterUserRequest registerRequest);

        Task<OperationResult<BlankResource>> UpdateUserAsync(UpdateUserRequest updateRequest);

        Task<OperationResult<BlankResource>> DeleteUserAsync(DeleteUserRequest deleteRequest);
    }
}
