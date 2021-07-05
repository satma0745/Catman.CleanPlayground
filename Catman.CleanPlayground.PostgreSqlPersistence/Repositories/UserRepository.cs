namespace Catman.CleanPlayground.PostgreSqlPersistence.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Catman.CleanPlayground.Application.Persistence.Entities;
    using Catman.CleanPlayground.Application.Persistence.Repositories;
    using Catman.CleanPlayground.PostgreSqlPersistence.Context;
    using Microsoft.EntityFrameworkCore;

    internal class UserRepository : IUserRepository
    {
        private readonly DatabaseContext _context;

        public UserRepository(DatabaseContext context)
        {
            _context = context;
        }

        public Task<bool> UserExistsAsync(Guid userId) =>
            _context.Users
                .AsNoTracking()
                .AnyAsync(user => user.Id == userId);

        public Task<bool> UserExistsAsync(string username) =>
            _context.Users
                .AsNoTracking()
                .AnyAsync(user => user.Username == username);
        
        public Task<bool> UsernameIsAvailableAsync(string username, Guid? userIdToExclude) =>
            _context.Users
                .AsNoTracking()
                .Where(user => user.Id != userIdToExclude)
                .AllAsync(user => user.Username != username);

        public async Task<bool> UserHasPasswordAsync(Guid userId, string passwordHash)
        {
            var userEntity = await _context.Users
                .AsNoTracking()
                .SingleAsync(user => user.Id == userId);
            
            return userEntity.Password.Hash == passwordHash;
        }

        public Task<UserEntity> GetUserAsync(Guid userId) =>
            _context.Users
                .AsNoTracking()
                .SingleAsync(user => user.Id == userId);

        public Task<UserEntity> GetUserAsync(string username) =>
            _context.Users
                .AsNoTracking()
                .SingleAsync(user => user.Username == username);

        public async Task<ICollection<UserEntity>> GetUsersAsync()
        {
            var userEntities = _context.Users.AsNoTracking();
            return await userEntities.ToListAsync();
        }

        public Task CreateUserAsync(UserEntity user)
        {
            _context.Users.Add(user);
            return _context.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(UserEntity updatedUser)
        {
            _context.Users.Update(updatedUser);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveUserAsync(Guid userId)
        {
            var userToDelete = await _context.Users
                .AsNoTracking()
                .SingleAsync(user => user.Id == userId);
            
            _context.Users.Remove(userToDelete);

            await _context.SaveChangesAsync();
        }
    }
}
