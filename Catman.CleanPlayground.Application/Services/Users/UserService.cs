namespace Catman.CleanPlayground.Application.Services.Users
{
    using System;
    using System.Collections.Generic;
    using AutoMapper;
    using Catman.CleanPlayground.Application.Data.Users;

    internal class UserService : IUserService
    {
        private readonly IUserRepository _users;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _users = userRepository;
            _mapper = mapper;
        }

        public bool UserExists(byte userId) =>
            _users.UserExists(userId);

        public ICollection<UserModel> GetUsers() =>
            _mapper.Map<ICollection<UserModel>>(_users.GetUsers());

        public void RegisterUser(RegisterUserModel registerModel)
        {
            var checkParams = new UsernameAvailabilityCheckParameters(registerModel.Username);
            if (!_users.UsernameIsAvailable(checkParams))
            {
                throw new Exception("Username already taken.");
            }

            var createData = _mapper.Map<UserCreateData>(registerModel);
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

            var updateData = _mapper.Map<UserUpdateData>(updateModel);
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
