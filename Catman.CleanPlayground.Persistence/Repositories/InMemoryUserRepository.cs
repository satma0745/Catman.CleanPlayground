namespace Catman.CleanPlayground.Persistence.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using Catman.CleanPlayground.Application.Data.Users;
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

        public bool UserExists(byte userId) =>
            _users.Any(user => user.Id == userId);
        
        public bool UsernameIsAvailable(UsernameAvailabilityCheckParameters checkParameters) =>
            _users
                .Where(user => user.Id != checkParameters.ExceptUserWithId)
                .All(user => user.Username != checkParameters.Username);

        public UserData GetUser(byte userId)
        {
            var userEntity = _users.Single(user => user.Id == userId);
            
            var userData = _mapper.Map<UserData>(userEntity);
            return userData;
        }

        public ICollection<UserData> GetUsers() =>
            _mapper.Map<ICollection<UserData>>(_users);

        public void CreateUser(UserCreateData createData)
        {
            var userId = _nextId;
            _nextId += 1;

            var user = _mapper.Map<UserEntity>(createData);
            user.Id = userId;
            
            _users.Add(user);
        }

        public void UpdateUser(UserUpdateData updateData)
        {
            var userToUpdate = _users.Single(user => user.Id == updateData.Id);
            _mapper.Map(updateData, userToUpdate);
        }

        public void RemoveUser(byte userId)
        {
            var userToDelete = _users.Single(user => user.Id == userId);
            _users.Remove(userToDelete);
        }
    }
}
