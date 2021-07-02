namespace Catman.CleanPlayground.Application.Services.Users.Operations
{
    using System;
    using System.Threading.Tasks;
    using Catman.CleanPlayground.Application.Persistence.Users;
    using Catman.CleanPlayground.Application.Services.Common.Response;
    using Catman.CleanPlayground.Application.Services.Common.Response.Errors;

    internal class DeleteUserOperationHandler
    {
        private readonly IUserRepository _userRepository;

        public DeleteUserOperationHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<OperationResult<OperationSuccess>> HandleAsync(byte userId)
        {
            try
            {
                if (!await _userRepository.UserExistsAsync(userId))
                {
                    var notFoundError = new NotFoundError("User not found.");
                    return new OperationResult<OperationSuccess>(notFoundError);
                }
            
                await _userRepository.RemoveUserAsync(userId);
                
                return new OperationResult<OperationSuccess>(new OperationSuccess());
            }
            catch (Exception exception)
            {
                var fatalError = new FatalError(exception);
                return new OperationResult<OperationSuccess>(fatalError);
            }
        }
    }
}
