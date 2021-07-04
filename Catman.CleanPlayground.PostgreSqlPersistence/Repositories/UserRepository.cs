namespace Catman.CleanPlayground.PostgreSqlPersistence.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using Catman.CleanPlayground.Application.Persistence.Users;
    using Catman.CleanPlayground.PostgreSqlPersistence.Context;
    using Catman.CleanPlayground.PostgreSqlPersistence.Entities;
    using Catman.CleanPlayground.PostgreSqlPersistence.Helpers;
    using Microsoft.EntityFrameworkCore;

    internal class UserRepository : IUserRepository
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public UserRepository(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Task<bool> UserExistsAsync(Guid userId) =>
            _context.Users
                .AsNoTracking()
                .AnyAsync(user => user.Id == userId);

        public Task<bool> UserExistsAsync(string username) =>
            _context.Users
                .AsNoTracking()
                .AnyAsync(user => user.Username == username);
        
        public Task<bool> UsernameIsAvailableAsync(UsernameAvailabilityCheckParameters checkParameters) =>
            _context.Users
                .AsNoTracking()
                .Where(user => user.Id != checkParameters.ExceptUserWithId)
                .AllAsync(user => user.Username != checkParameters.Username);

        public async Task<bool> UserHasPasswordAsync(Guid userId, string password)
        {
            var userEntity = await _context.Users
                .AsNoTracking()
                .SingleAsync(user => user.Id == userId);
            
            return PasswordHelper.VerifyPassword(userEntity.Password, password);
        }

        public async Task<UserData> GetUserAsync(Guid userId)
        {
            var userEntity = await _context.Users
                .AsNoTracking()
                .SingleAsync(user => user.Id == userId);
            
            return _mapper.Map<UserData>(userEntity);
        }

        public async Task<UserData> GetUserAsync(string username)
        {
            var userEntity = await _context.Users
                .AsNoTracking()
                .SingleAsync(user => user.Username == username);
            
            return _mapper.Map<UserData>(userEntity);
        }

        public async Task<ICollection<UserData>> GetUsersAsync()
        {
            var userEntities = await _context.Users
                .AsNoTracking()
                .ToListAsync();
            
            return _mapper.Map<ICollection<UserData>>(userEntities);
        }

        public async Task CreateUserAsync(UserCreateData createData)
        {
            var user = _mapper.Map<UserEntity>(createData);
            user.Id = Guid.NewGuid();
            user.Password = PasswordHelper.HashPassword(createData.Password);
            
            _context.Users.Add(user);
            
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(UserUpdateData updateData)
        {
            var userToUpdate = await _context.Users.SingleAsync(user => user.Id == updateData.Id);
            
            _mapper.Map(updateData, userToUpdate);
            userToUpdate.Password = PasswordHelper.HashPassword(updateData.Password);
            
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
