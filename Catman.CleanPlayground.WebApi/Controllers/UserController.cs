namespace Catman.CleanPlayground.WebApi.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper;
    using Catman.CleanPlayground.Application.UseCases.Common.Response.Errors;
    using Catman.CleanPlayground.Application.UseCases.Users.DeleteUser;
    using Catman.CleanPlayground.Application.UseCases.Users.GetUsers;
    using Catman.CleanPlayground.Application.UseCases.Users.RegisterUser;
    using Catman.CleanPlayground.Application.UseCases.Users.UpdateUser;
    using Catman.CleanPlayground.WebApi.DataTransferObjects.User;
    using Catman.CleanPlayground.WebApi.Extensions.UseCases;
    using MediatR;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    [Route("/api/users")]
    public class UserController : ApiControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public UserController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ICollection<UserDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> GetUsersAsync() =>
            _mediator
                .Send(new GetUsersRequest())
                .SelectActionResultAsync(
                    users => Ok(_mapper.Map<ICollection<UserDto>>(users)),
                    _ => InternalServerError());

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> RegisterUserAsync([FromBody] RegisterUserDto registerDto) =>
            _mediator
                .Send(_mapper.Map<RegisterUserRequest>(registerDto))
                .SelectActionResultAsync(
                    () => Ok(),
                    error => error switch
                    {
                        ValidationError validationError => BadRequest(validationError.ValidationErrors),
                        _ => InternalServerError()
                    });

        [HttpPost("{userId:guid}/update")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> UpdateUserAsync([FromRoute] Guid userId, [FromBody] UpdateUserDto updateDto) =>
            _mediator
                .Send(() =>
                {
                    var updateRequest = new UpdateUserRequest(userId, AuthorizationToken);
                    return _mapper.Map(updateDto, updateRequest);
                })
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

        [HttpGet("{userId:guid}/delete")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> DeleteUserAsync([FromRoute] Guid userId) =>
            _mediator
                .Send(new DeleteUserRequest(userId, AuthorizationToken))
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
}
