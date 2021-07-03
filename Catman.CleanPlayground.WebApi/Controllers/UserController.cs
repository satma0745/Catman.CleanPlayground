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
    using Catman.CleanPlayground.Application.Services.Users.Requests;
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
            
            var registerRequest = _mapper.Map<RegisterUserRequest>(registerDto);
            var operationResult = await _userService.RegisterUserAsync(registerRequest);
            
            return operationResult.Select(
                onSuccess: () => Ok(),
                onFailure: error => error switch
                {                  
                    ValidationError validationError => BadRequest(validationError.ValidationErrors),
                    _ => (IActionResult) StatusCode(500)
                });
        }

        [HttpPost("{id:guid}/update")]
        public async Task<IActionResult> UpdateUserAsync(
            [FromRoute] Guid id,
            [FromHeader] string authorization,
            [FromBody] UpdateUserDto updateDto)
        {
            var validationResult = await _updateDtoValidator.ValidateAsync(updateDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.GetValidationErrors());
            }
            
            var updateRequest = _mapper.Map<UpdateUserRequest>(updateDto);
            updateRequest.Id = id;
            updateRequest.AuthenticationToken = authorization;

            var operationResult = await _userService.UpdateUserAsync(updateRequest);
            
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
        public async Task<IActionResult> DeleteUserAsync([FromRoute] Guid userId, [FromHeader] string authorization)
        {
            var deleteRequest = new DeleteUserRequest
            {
                Id = userId,
                AuthenticationToken = authorization
            };
            var operationResult = await _userService.DeleteUserAsync(deleteRequest);
            
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
