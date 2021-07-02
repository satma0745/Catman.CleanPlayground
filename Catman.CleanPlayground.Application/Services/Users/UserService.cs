namespace Catman.CleanPlayground.Application.Services.Users
{
    using System;
    using System.Collections.Generic;
    using AutoMapper;
    using Catman.CleanPlayground.Application.Data.Users;
    using Catman.CleanPlayground.Application.Services.Common.Response;

    internal class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public OperationResult<ICollection<UserModel>> GetUsers()
        {
            try
            {
                var usersData = _userRepository.GetUsers();
                var users = _mapper.Map<ICollection<UserModel>>(usersData);
                return new OperationResult<ICollection<UserModel>>(users);
            }
            catch (Exception exception)
            {
                var fatalError = new Error(exception.Message);
                return new OperationResult<ICollection<UserModel>>(fatalError);
            }
        }

        public OperationResult RegisterUser(RegisterUserModel registerModel)
        {
            try
            {
                var checkParams = new UsernameAvailabilityCheckParameters(registerModel.Username);
                if (!_userRepository.UsernameIsAvailable(checkParams))
                {
                    var conflictError = new Error("Username already taken.");
                    return new OperationResult(conflictError);
                }

                var createData = _mapper.Map<UserCreateData>(registerModel);
                _userRepository.CreateUser(createData);

                return new OperationResult();
            }
            catch (Exception exception)
            {
                var fatalError = new Error(exception.Message);
                return new OperationResult(fatalError);
            }
        }

        public OperationResult UpdateUser(UpdateUserModel updateModel)
        {
            try
            {
                if (!UserExists(updateModel.Id))
                {
                    var notFoundError = new Error("User not found.");
                    return new OperationResult(notFoundError);
                }

                var checkParameters = new UsernameAvailabilityCheckParameters(updateModel.Username, updateModel.Id);
                if (!_userRepository.UsernameIsAvailable(checkParameters))
                {
                    var conflictError = new Error("Username already taken.");
                    return new OperationResult(conflictError);
                }

                var updateData = _mapper.Map<UserUpdateData>(updateModel);
                _userRepository.UpdateUser(updateData);

                return new OperationResult();
            }
            catch (Exception exception)
            {
                var fatalError = new Error(exception.Message);
                return new OperationResult(fatalError);
            }
        }

        public OperationResult DeleteUser(byte userId)
        {
            try
            {
                if (!UserExists(userId))
                {
                    var notFoundError = new Error("User not found.");
                    return new OperationResult(notFoundError);
                }
            
                _userRepository.RemoveUser(userId);
                
                return new OperationResult();
            }
            catch (Exception exception)
            {
                var fatalError = new Error(exception.Message);
                return new OperationResult(fatalError);
            }
        }
        
        private bool UserExists(byte userId) =>
            _userRepository.UserExists(userId);
    }
}
