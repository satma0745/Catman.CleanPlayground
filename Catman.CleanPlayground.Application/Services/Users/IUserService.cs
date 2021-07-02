namespace Catman.CleanPlayground.Application.Services.Users
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Catman.CleanPlayground.Application.Services.Common.Response;
    using Catman.CleanPlayground.Application.Services.Users.Models;

    public interface IUserService
    {
        Task<OperationResult<ICollection<UserModel>>> GetUsersAsync();

        Task<OperationResult> RegisterUserAsync(RegisterUserModel registerModel);

        Task<OperationResult> UpdateUserAsync(UpdateUserModel updateModel);

        Task<OperationResult> DeleteUserAsync(byte userId);
    }
}
