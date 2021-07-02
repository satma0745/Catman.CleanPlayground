namespace Catman.CleanPlayground.Application.Persistence.Users
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IUserRepository
    {
        Task<bool> UserExistsAsync(Guid userId);

        Task<bool> UsernameIsAvailableAsync(UsernameAvailabilityCheckParameters checkParameters);

        Task<ICollection<UserData>> GetUsersAsync();

        Task CreateUserAsync(UserCreateData createData);

        Task UpdateUserAsync(UserUpdateData updateData);

        Task RemoveUserAsync(Guid userId);
    }
}
