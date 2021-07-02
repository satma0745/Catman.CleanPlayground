namespace Catman.CleanPlayground.WebApi.Controllers
{
    using System;
    using Catman.CleanPlayground.Application.Services.Users;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("/api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            try
            {
                var users = _userService.GetUsers();
                return Ok(users);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPost("register")]
        public IActionResult RegisterUser(RegisterUserModel registerModel)
        {
            try
            {
                _userService.RegisterUser(registerModel);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPost("update")]
        public IActionResult UpdateUser(UpdateUserModel updateModel)
        {
            try
            {
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
