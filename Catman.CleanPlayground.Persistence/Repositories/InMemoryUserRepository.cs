namespace Catman.CleanPlayground.Persistence.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using Catman.CleanPlayground.Application.Data.Users;
    using Catman.CleanPlayground.Persistence.Entities;

    internal class InMemoryUserRepository : IUserRepository
    {
        private readonly ICollection<UserEntity> _users = new List<UserEntity>();

        private byte _nextId = 1;

        public bool UserExists(byte userId) =>
            _users.Any(user => user.Id == userId);
        
        public bool UsernameIsAvailable(UsernameAvailabilityCheckParameters checkParameters) =>
            _users
                .Where(user => user.Id != checkParameters.ExceptUserWithId)
                .All(user => user.Username != checkParameters.Username);

        public UserData GetUser(byte userId) =>
            _users
                .Select(userEntity => new UserData()
                {
                    Id = userEntity.Id,
                    Username = userEntity.Username,
                    DisplayName = userEntity.DisplayName
                })
                .Single(user => user.Id == userId);

        public ICollection<UserData> GetUsers() =>
            _users
                .Select(userEntity => new UserData()
                {
                    Id = userEntity.Id,
                    Username = userEntity.Username,
                    DisplayName = userEntity.DisplayName
                })
                .ToList();

        public void CreateUser(UserCreateData createData)
        {
            var userId = _nextId;
            _nextId += 1;

            var user = new UserEntity
            {
                Id = userId,
                Username = createData.Username,
                Password = createData.Password,
                DisplayName = createData.DisplayName
            };
            
            _users.Add(user);
        }

        public void UpdateUser(UserUpdateData updateData)
        {
            var userToUpdate = _users.Single(user => user.Id == updateData.Id);
            
            userToUpdate.Username = updateData.Username;
            userToUpdate.Password = updateData.Password;
            userToUpdate.DisplayName = updateData.DisplayName;
        }

        public void RemoveUser(byte userId)
        {
            var userToDelete = _users.Single(user => user.Id == userId);
            _users.Remove(userToDelete);
        }
    }
}
