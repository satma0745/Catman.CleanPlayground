namespace Catman.CleanPlayground.Data.Users
{
    using System.Collections.Generic;
    using System.Linq;

    internal class InMemoryUserRepository
    {
        private readonly ICollection<UserEntity> _users = new List<UserEntity>();

        private byte _nextId = 1;

        public bool UserExists(byte userId) =>
            _users.Any(user => user.Id == userId);
        
        public bool UsernameIsAvailable(string username, byte? exceptUserId = null) =>
            _users.Where(user => user.Id != exceptUserId).All(user => user.Username != username);
        
        public UserEntity GetUser(byte userId) =>
            _users.Single(user => user.Id == userId);

        public ICollection<UserEntity> GetUsers() =>
            _users.ToList();

        public void CreateUser(UserEntity user)
        {
            user.Id = _nextId;
            _nextId += 1;
            
            _users.Add(user);
        }

        public void UpdateUser(byte userId, UserEntity update)
        {
            var userToUpdate = GetUser(userId);
            
            userToUpdate.Username = update.Username;
            userToUpdate.Password = update.Password;
            userToUpdate.DisplayName = update.DisplayName;
        }

        public void RemoveUser(byte userId) =>
            _users.Remove(GetUser(userId));
    }
}
