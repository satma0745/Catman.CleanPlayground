namespace Catman.CleanPlayground.WebApi.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper;
    using Catman.CleanPlayground.Application.Extensions.Services;
    using Catman.CleanPlayground.Application.Services.Common.Response.Errors;
    using Catman.CleanPlayground.Application.Services.Users;
    using Catman.CleanPlayground.Application.Services.Users.Models;
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
                onSuccess: () => Ok(),
                onFailure: error => error switch
                {                  
                    ValidationError validationError => BadRequest(validationError.ValidationErrors),
                    _ => (IActionResult) StatusCode(500)
                });
        }

        [HttpPost("{id:guid}/update")]
        public async Task<IActionResult> UpdateUserAsync([FromRoute] Guid id, UpdateUserDto updateDto)
        {
            var updateModel = _mapper.Map<UpdateUserModel>(updateDto);
            updateModel.Id = id;

            var operationResult = await _userService.UpdateUserAsync(updateModel);
            
            return operationResult.Select(
                onSuccess: () => Ok(),
                onFailure: error => error switch
                {
                    ValidationError validationError => BadRequest(validationError.ValidationErrors),
                    NotFoundError => NotFound(),
                    _ => (IActionResult) StatusCode(500)
                });
        }

        [HttpGet("{userId:guid}/delete")]
        public async Task<IActionResult> DeleteUserAsync([FromRoute] Guid userId)
        {
            var operationResult = await _userService.DeleteUserAsync(userId);
            
            return operationResult.Select(
                onSuccess: () => Ok(),
                onFailure: error => error switch
                {
                    NotFoundError => NotFound(),
                    _ => (IActionResult) StatusCode(500)
                });
        }
    }
}
