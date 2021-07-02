namespace Catman.CleanPlayground.Application.Persistence.Users
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IUserRepository
    {
        Task<bool> UserExistsAsync(byte userId);

        Task<bool> UsernameIsAvailableAsync(UsernameAvailabilityCheckParameters checkParameters);

        Task<ICollection<UserData>> GetUsersAsync();

        Task CreateUserAsync(UserCreateData createData);

        Task UpdateUserAsync(UserUpdateData updateData);

        Task RemoveUserAsync(byte userId);
    }
}
