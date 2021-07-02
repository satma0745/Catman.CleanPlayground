namespace Catman.CleanPlayground.Application.Services.Users.Operations
{
    using System;
    using System.Threading.Tasks;
    using AutoMapper;
    using Catman.CleanPlayground.Application.Persistence.Users;
    using Catman.CleanPlayground.Application.Services.Common.Response;
    using Catman.CleanPlayground.Application.Services.Common.Response.Errors;
    using Catman.CleanPlayground.Application.Services.Users.Models;

    internal class UpdateUserOperationHandler
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UpdateUserOperationHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        
        public async Task<OperationResult> HandleAsync(UpdateUserModel updateModel)
        {
            try
            {
                if (!await _userRepository.UserExistsAsync(updateModel.Id))
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
    }
}
