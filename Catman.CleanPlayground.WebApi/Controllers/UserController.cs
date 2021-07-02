namespace Catman.CleanPlayground.WebApi.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
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
        public async Task<IActionResult> GetUsersAsync()
        {
            var operationResult = await _userService.GetUsersAsync();

            return operationResult.Select(
                onSuccess: users => Ok(_mapper.Map<ICollection<UserDto>>(users)),
                onFailure: _ => (IActionResult) StatusCode(500));
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUserAsync(RegisterUserDto registerDto)
        {
            var registerModel = _mapper.Map<RegisterUserModel>(registerDto);
            var operationResult = await _userService.RegisterUserAsync(registerModel);
            
            return operationResult.Select(
                onSuccess: _ => Ok(),
                onFailure: _ => StatusCode(500));
        }

        [HttpPost("{id}/update")]
        public async Task<IActionResult> UpdateUserAsync([FromRoute] byte id, UpdateUserDto updateDto)
        {
            var updateModel = _mapper.Map<UpdateUserModel>(updateDto);
            updateModel.Id = id;

            var operationResult = await _userService.UpdateUserAsync(updateModel);
            
            return operationResult.Select(
                onSuccess: _ => Ok(),
                onFailure: _ => StatusCode(500));
        }

        [HttpGet("{userId}/delete")]
        public async Task<IActionResult> DeleteUserAsync([FromRoute] byte userId)
        {
            var operationResult = await _userService.DeleteUserAsync(userId);
            
            return operationResult.Select(
                onSuccess: _ => Ok(),
                onFailure: _ => StatusCode(500));
        }
    }
}
