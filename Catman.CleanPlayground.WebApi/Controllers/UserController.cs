namespace Catman.CleanPlayground.WebApi.Controllers
{
    using System.Collections.Generic;
    using AutoMapper;
    using Catman.CleanPlayground.Application.Extensions.Services;
    using Catman.CleanPlayground.Application.Services.Users;
    using Catman.CleanPlayground.WebApi.DataObjects.User;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("/api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetUsers() =>
            _userService
                .GetUsers()
                .Select<ICollection<UserModel>, IActionResult>(
                    onSuccess: users => Ok(_mapper.Map<ICollection<UserDto>>(users)),
                    onFailure: _ => StatusCode(500));

        [HttpPost("register")]
        public IActionResult RegisterUser(RegisterUserDto registerDto) =>
            _userService
                .RegisterUser(_mapper.Map<RegisterUserModel>(registerDto))
                .Select(onSuccess: _ => Ok(), onFailure: _ => StatusCode(500));

        [HttpPost("{id}/update")]
        public IActionResult UpdateUser([FromRoute] byte id, UpdateUserDto updateDto)
        {
            var updateModel = _mapper.Map<UpdateUserModel>(updateDto);
            updateModel.Id = id;

            return _userService
                .UpdateUser(updateModel)
                .Select(onSuccess: _ => Ok(), onFailure: _ => StatusCode(500));
        }

        [HttpGet("{userId}/delete")]
        public IActionResult DeleteUser([FromRoute] byte userId) =>
            _userService
                .DeleteUser(userId)
                .Select(onSuccess: _ => Ok(), onFailure: _ => StatusCode(500));
    }
}
