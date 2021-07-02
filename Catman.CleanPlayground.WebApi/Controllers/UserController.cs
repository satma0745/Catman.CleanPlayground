namespace Catman.CleanPlayground.WebApi.Controllers
{
    using System;
    using System.Collections.Generic;
    using AutoMapper;
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
        public IActionResult GetUsers()
        {
            try
            {
                var users = _userService.GetUsers();
                
                var dtos = _mapper.Map<ICollection<UserDto>>(users);
                return Ok(dtos);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPost("register")]
        public IActionResult RegisterUser(RegisterUserDto registerDto)
        {
            try
            {
                var registerModel = _mapper.Map<RegisterUserModel>(registerDto);
                _userService.RegisterUser(registerModel);
                
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPost("{id}/update")]
        public IActionResult UpdateUser([FromRoute] byte id, UpdateUserDto updateDto)
        {
            try
            {
                var updateModel = _mapper.Map<UpdateUserModel>(updateDto);
                updateModel.Id = id;
                
                _userService.UpdateUser(updateModel);
                
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("{userId}/delete")]
        public IActionResult DeleteUser([FromRoute] byte userId)
        {
            try
            {
                _userService.DeleteUser(userId);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
