namespace Catman.CleanPlayground.Application.Services.Users
{
    using System.Collections.Generic;
    using Catman.CleanPlayground.Application.Services.Common.Response;

    public interface IUserService
    {
        OperationResult<ICollection<UserModel>> GetUsers();

        OperationResult RegisterUser(RegisterUserModel registerModel);

        OperationResult UpdateUser(UpdateUserModel updateModel);

        OperationResult DeleteUser(byte userId);
    }
}
