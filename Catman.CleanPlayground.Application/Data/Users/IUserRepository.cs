namespace Catman.CleanPlayground.Application.Data.Users
{
    using System.Collections.Generic;

    public interface IUserRepository
    {
        bool UserExists(byte userId);

        bool UsernameIsAvailable(UsernameAvailabilityCheckParameters checkParameters);

        UserData GetUser(byte userId);

        ICollection<UserData> GetUsers();

        void CreateUser(UserCreateData createData);

        void UpdateUser(UserUpdateData updateData);

        void RemoveUser(byte userId);
    }
}
