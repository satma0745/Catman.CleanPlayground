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

        public Task<UserEntity> GetUserAsync(Guid userId) =>
            _context.Users.SingleAsync(user => user.Id == userId);

        public Task<UserEntity> GetUserAsync(string username) =>
            _context.Users.SingleAsync(user => user.Username == username);

        public async Task<ICollection<UserEntity>> GetUsersAsync() =>
            await _context.Users.ToListAsync();

        public Task CreateUserAsync(UserEntity user)
        {
            _context.Users.Add(user);
            return _context.SaveChangesAsync();
        }

        public Task RemoveUserAsync(UserEntity user)
        {
            _context.Users.Remove(user);
            return _context.SaveChangesAsync();
        }
    }
}
