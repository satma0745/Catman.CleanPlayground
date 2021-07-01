namespace Catman.CleanPlayground.Services.Users
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Catman.CleanPlayground.Data.Users;

    internal class UserService
    {
        private readonly InMemoryUserRepository _users;

        public UserService(InMemoryUserRepository userRepository)
        {
            _users = userRepository;
        }

        public bool UserExists(byte userId) =>
            _users.UserExists(userId);

        public ICollection<UserModel> GetUsers() =>
            _users.GetUsers()
                .Select(userEntity => new UserModel
                {
                    Id = userEntity.Id,
                    Username = userEntity.Username,
                    DisplayName = userEntity.DisplayName
                })
                .ToList();

        public void RegisterUser(RegisterUserModel registerModel)
        {
            if (!_users.UsernameIsAvailable(registerModel.Username))
            {
                throw new Exception("Username already taken.");
            }

            var userEntity = new UserEntity
            {
                Username = registerModel.Username,
                Password = registerModel.Password,
                DisplayName = registerModel.DisplayName
            };
            _users.CreateUser(userEntity);
        }

        public void UpdateUser(UpdateUserModel updateModel)
        {
            if (!UserExists(updateModel.Id))
            {
                throw new Exception("User not found.");
            }
            if (!_users.UsernameIsAvailable(updateModel.Username, updateModel.Id))
            {
                throw new Exception("Username already taken.");
            }

            var entityUpdate = new UserEntity
            {
                Username = updateModel.Username,
                Password = updateModel.Password,
                DisplayName = updateModel.DisplayName
            };
            _users.UpdateUser(updateModel.Id, entityUpdate);
        }

        public void DeleteUser(byte userId)
        {
            if (!UserExists(userId))
            {
                throw new Exception("User not found.");
            }
            
            _users.RemoveUser(userId);
        }
    }
}
