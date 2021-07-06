namespace Catman.CleanPlayground.WebApi.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper;
    using Catman.CleanPlayground.Application.UseCases.Common.Response.Errors;
    using Catman.CleanPlayground.Application.UseCases.Users;
    using Catman.CleanPlayground.Application.UseCases.Users.DeleteUser;
    using Catman.CleanPlayground.Application.UseCases.Users.RegisterUser;
    using Catman.CleanPlayground.Application.UseCases.Users.UpdateUser;
    using Catman.CleanPlayground.WebApi.DataTransferObjects.User;
    using Catman.CleanPlayground.WebApi.Extensions.Services;
    using Microsoft.AspNetCore.Mvc;

    [Route("/api/users")]
    public class UserController : ApiControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        public Task<IActionResult> GetUsersAsync() =>
            _userService
                .GetUsersAsync()
                .SelectActionResultAsync(
                    users => Ok(_mapper.Map<ICollection<UserDto>>(users)),
                    _ => InternalServerError());

        [HttpPost("register")]
        public Task<IActionResult> RegisterUserAsync([FromBody] RegisterUserDto registerDto) =>
            _userService
                .RegisterUserAsync(_mapper.Map<RegisterUserRequest>(registerDto))
                .SelectActionResultAsync(
                    () => Ok(),
                    error => error switch
                    {
                        ValidationError validationError => BadRequest(validationError.ValidationErrors),
                        _ => InternalServerError()
                    });

        [HttpPost("{id:guid}/update")]
        public async Task<IActionResult> UpdateUserAsync(
            [FromRoute] Guid id,
            [FromHeader] string authorization,
            [FromBody] UpdateUserDto updateDto)
        {
            var updateRequest = new UpdateUserRequest(id, authorization);
            _mapper.Map(updateDto, updateRequest);
            
            return await _userService
                .UpdateUserAsync(updateRequest)
                .SelectActionResultAsync(
                    () => Ok(),
                    error => error switch
                    {
                        ValidationError validationError => BadRequest(validationError.ValidationErrors),
                        AuthenticationError => Unauthorized(),
                        AccessViolationError => Forbidden(),
                        NotFoundError => NotFound(),
                        _ => InternalServerError()
                    });
        }

        [HttpGet("{userId:guid}/delete")]
        public Task<IActionResult> DeleteUserAsync([FromRoute] Guid userId, [FromHeader] string authorization) =>
            _userService
                .DeleteUserAsync(new DeleteUserRequest(userId, authorization))
                .SelectActionResultAsync(
                    () => Ok(),
                    error => error switch
                    {
                        ValidationError validationError => BadRequest(validationError.ValidationErrors),
                        NotFoundError => NotFound(),
                        AuthenticationError => Unauthorized(),
                        AccessViolationError => Forbidden(),
                        _ => InternalServerError()
                    });
    }
}
