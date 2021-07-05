namespace Catman.CleanPlayground.Application.Persistence.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Catman.CleanPlayground.Application.Persistence.Entities;

    public interface IUserRepository
    {
        Task<bool> UserExistsAsync(Guid userId);

        Task<bool> UserExistsAsync(string username);

        Task<bool> UsernameIsAvailableAsync(string username, Guid? userIdToExclude = null);

        Task<UserEntity> GetUserAsync(Guid userId);

        Task<UserEntity> GetUserAsync(string username);

        Task<ICollection<UserEntity>> GetUsersAsync();

        Task CreateUserAsync(UserEntity user);

        Task RemoveUserAsync(UserEntity user);
    }
}
