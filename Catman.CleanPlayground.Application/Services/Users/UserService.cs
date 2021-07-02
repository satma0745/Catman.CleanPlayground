namespace Catman.CleanPlayground.Application.Services.Users
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Catman.CleanPlayground.Application.Data.Users;

    internal class UserService : IUserService
    {
        private readonly IUserRepository _users;

        public UserService(IUserRepository userRepository)
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
            var checkParams = new UsernameAvailabilityCheckParameters(registerModel.Username);
            if (!_users.UsernameIsAvailable(checkParams))
            {
                throw new Exception("Username already taken.");
            }

            var createData = new UserCreateData
            {
                Username = registerModel.Username,
                Password = registerModel.Password,
                DisplayName = registerModel.DisplayName
            };
            _users.CreateUser(createData);
        }

        public void UpdateUser(UpdateUserModel updateModel)
        {
            if (!UserExists(updateModel.Id))
            {
                throw new Exception("User not found.");
            }

            var checkParameters = new UsernameAvailabilityCheckParameters(updateModel.Username, updateModel.Id); 
            if (!_users.UsernameIsAvailable(checkParameters))
            {
                throw new Exception("Username already taken.");
            }

            var updateData = new UserUpdateData
            {
                Id = updateModel.Id,
                Username = updateModel.Username,
                Password = updateModel.Password,
                DisplayName = updateModel.DisplayName
            };
            _users.UpdateUser(updateData);
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
