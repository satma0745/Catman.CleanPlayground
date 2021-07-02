namespace Catman.CleanPlayground.Application.Services.Users
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Catman.CleanPlayground.Application.Services.Common.Response;
    using Catman.CleanPlayground.Application.Services.Users.Models;

    public interface IUserService
    {
        Task<OperationResult<ICollection<UserModel>>> GetUsersAsync();

        Task<OperationResult<OperationSuccess>> RegisterUserAsync(RegisterUserModel registerModel);

        Task<OperationResult<OperationSuccess>> UpdateUserAsync(UpdateUserModel updateModel);

        Task<OperationResult<OperationSuccess>> DeleteUserAsync(Guid userId);
    }
}
