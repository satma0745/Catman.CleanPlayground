namespace Catman.CleanPlayground.WebApi.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper;
    using Catman.CleanPlayground.Application.Extensions.Services;
    using Catman.CleanPlayground.Application.Extensions.Validation;
    using Catman.CleanPlayground.Application.Services.Common.Response.Errors;
    using Catman.CleanPlayground.Application.Services.Users;
    using Catman.CleanPlayground.Application.Services.Users.Models;
    using Catman.CleanPlayground.WebApi.DataTransferObjects.User;
    using FluentValidation;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("/api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        private readonly IValidator<RegisterUserDto> _registerDtoValidator;
        private readonly IValidator<UpdateUserDto> _updateDtoValidator;
        
        private readonly IMapper _mapper;

        public UserController(
            IUserService userService,
            IValidator<RegisterUserDto> registerDtoValidator,
            IValidator<UpdateUserDto> updateDtoValidator,
            IMapper mapper)
        {
            _userService = userService;

            _registerDtoValidator = registerDtoValidator;
            _updateDtoValidator = updateDtoValidator;
            
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
        public async Task<IActionResult> RegisterUserAsync([FromBody] RegisterUserDto registerDto)
        {
            var validationResult = await _registerDtoValidator.ValidateAsync(registerDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.GetValidationErrors());
            }
            
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
        public async Task<IActionResult> UpdateUserAsync([FromRoute] Guid id, [FromBody] UpdateUserDto updateDto)
        {
            var validationResult = await _updateDtoValidator.ValidateAsync(updateDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.GetValidationErrors());
            }
            
            var updateModel = _mapper.Map<UpdateUserModel>(updateDto);
            updateModel.Id = id;

            var operationResult = await _userService.UpdateUserAsync(updateModel);
            
            return operationResult.Select(
                onSuccess: () => Ok(),
                onFailure: error => error switch
                {
                    ValidationError validationError => BadRequest(validationError.ValidationErrors),
                    AuthenticationError => Unauthorized(),
                    AccessViolationError => StatusCode(403),
                    NotFoundError => NotFound(),
                    _ => (IActionResult) StatusCode(500)
                });
        }

        [HttpGet("{userId:guid}/delete")]
        public async Task<IActionResult> DeleteUserAsync([FromRoute] Guid userId, [FromBody] DeleteUserDto deleteDto)
        {
            var deleteModel = new DeleteUserModel
            {
                Id = userId,
                AuthenticationToken = deleteDto.AuthenticationToken
            };
            var operationResult = await _userService.DeleteUserAsync(deleteModel);
            
            return operationResult.Select(
                onSuccess: () => Ok(),
                onFailure: error => error switch
                {
                    ValidationError validationError => BadRequest(validationError.ValidationErrors),
                    NotFoundError => NotFound(),
                    AuthenticationError => Unauthorized(),
                    AccessViolationError => StatusCode(403),
                    _ => (IActionResult) StatusCode(500)
                });
        }
    }
}
