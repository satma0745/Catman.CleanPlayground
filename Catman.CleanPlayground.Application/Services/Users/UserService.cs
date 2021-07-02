namespace Catman.CleanPlayground.Application.Services.Users
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper;
    using Catman.CleanPlayground.Application.Persistence.Users;
    using Catman.CleanPlayground.Application.Services.Common.Response;
    using Catman.CleanPlayground.Application.Services.Common.Response.Errors;

    internal class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<OperationResult<ICollection<UserModel>>> GetUsersAsync()
        {
            try
            {
                var usersData = await _userRepository.GetUsersAsync();
                var users = _mapper.Map<ICollection<UserModel>>(usersData);
                return new OperationResult<ICollection<UserModel>>(users);
            }
            catch (Exception exception)
            {
                var fatalError = new FatalError(exception);
                return new OperationResult<ICollection<UserModel>>(fatalError);
            }
        }

        public async Task<OperationResult> RegisterUserAsync(RegisterUserModel registerModel)
        {
            try
            {
                var checkParams = new UsernameAvailabilityCheckParameters(registerModel.Username);
                if (!await _userRepository.UsernameIsAvailableAsync(checkParams))
                {
                    var conflictError = new ConflictError("Username already taken.");
                    return new OperationResult(conflictError);
                }

                var createData = _mapper.Map<UserCreateData>(registerModel);
                await _userRepository.CreateUserAsync(createData);

                return new OperationResult();
            }
            catch (Exception exception)
            {
                var fatalError = new FatalError(exception);
                return new OperationResult(fatalError);
            }
        }

        public async Task<OperationResult> UpdateUserAsync(UpdateUserModel updateModel)
        {
            try
            {
                if (!await UserExists(updateModel.Id))
                {
                    var notFoundError = new NotFoundError("User not found.");
                    return new OperationResult(notFoundError);
                }

                var checkParameters = new UsernameAvailabilityCheckParameters(updateModel.Username, updateModel.Id);
                if (!await _userRepository.UsernameIsAvailableAsync(checkParameters))
                {
                    var conflictError = new ConflictError("Username already taken.");
                    return new OperationResult(conflictError);
                }

                var updateData = _mapper.Map<UserUpdateData>(updateModel);
                await _userRepository.UpdateUserAsync(updateData);

                return new OperationResult();
            }
            catch (Exception exception)
            {
                var fatalError = new FatalError(exception);
                return new OperationResult(fatalError);
            }
        }

        public async Task<OperationResult> DeleteUserAsync(byte userId)
        {
            try
            {
                if (!await UserExists(userId))
                {
                    var notFoundError = new NotFoundError("User not found.");
                    return new OperationResult(notFoundError);
                }
            
                await _userRepository.RemoveUserAsync(userId);
                
                return new OperationResult();
            }
            catch (Exception exception)
            {
                var fatalError = new FatalError(exception);
                return new OperationResult(fatalError);
            }
        }
        
        private Task<bool> UserExists(byte userId) =>
            _userRepository.UserExistsAsync(userId);
    }
}
