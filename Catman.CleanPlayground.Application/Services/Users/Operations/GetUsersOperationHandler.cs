namespace Catman.CleanPlayground.Application.Services.Users.Operations
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper;
    using Catman.CleanPlayground.Application.Persistence.Users;
    using Catman.CleanPlayground.Application.Services.Common.Response;
    using Catman.CleanPlayground.Application.Services.Common.Response.Errors;
    using Catman.CleanPlayground.Application.Services.Users.Models;

    internal class GetUsersOperationHandler
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetUsersOperationHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<OperationResult<ICollection<UserModel>>> HandleAsync()
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
    }
}