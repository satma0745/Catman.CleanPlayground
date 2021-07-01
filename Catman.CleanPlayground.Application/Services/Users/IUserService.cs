namespace Catman.CleanPlayground.Application.Services.Users
{
    using System.Collections.Generic;

    public interface IUserService
    {
        bool UserExists(byte userId);

        ICollection<UserModel> GetUsers();

        void RegisterUser(RegisterUserModel registerModel);

        void UpdateUser(UpdateUserModel updateModel);

        void DeleteUser(byte userId);
    }
}
