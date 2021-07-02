namespace Catman.CleanPlayground.Persistence.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using Catman.CleanPlayground.Application.Persistence.Users;
    using Catman.CleanPlayground.Persistence.Entities;

    internal class InMemoryUserRepository : IUserRepository
    {
        private readonly ICollection<UserEntity> _users = new List<UserEntity>();
        private readonly IMapper _mapper;

        private byte _nextId = 1;

        public InMemoryUserRepository(IMapper mapper)
        {
            _mapper = mapper;
        }

        public Task<bool> UserExistsAsync(byte userId)
        {
            var userExists = _users.Any(user => user.Id == userId);
            return Task.FromResult(userExists);
        }
        
        public Task<bool> UsernameIsAvailableAsync(UsernameAvailabilityCheckParameters checkParameters)
        {
            var isAvailable = _users
                .Where(user => user.Id != checkParameters.ExceptUserWithId)
                .All(user => user.Username != checkParameters.Username);

            return Task.FromResult(isAvailable);
        }

        public Task<ICollection<UserData>> GetUsersAsync()
        {
            var users = _mapper.Map<ICollection<UserData>>(_users);
            return Task.FromResult(users);
        }

        public Task CreateUserAsync(UserCreateData createData)
        {
            var userId = _nextId;
            _nextId += 1;

            var user = _mapper.Map<UserEntity>(createData);
            user.Id = userId;
            
            _users.Add(user);
            
            return Task.CompletedTask;
        }

        public Task UpdateUserAsync(UserUpdateData updateData)
        {
            var userToUpdate = _users.Single(user => user.Id == updateData.Id);
            _mapper.Map(updateData, userToUpdate);
            
            return Task.CompletedTask;
        }

        public Task RemoveUserAsync(byte userId)
        {
            var userToDelete = _users.Single(user => user.Id == userId);
            _users.Remove(userToDelete);
            
            return Task.CompletedTask;
        }
    }
}
