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
        public Task<IActionResult> GetUsersAsync() =>
            _mediator
                .Send(new GetUsersRequest())
                .SelectActionResultAsync(
                    users => Ok(_mapper.Map<ICollection<UserDto>>(users)),
                    _ => InternalServerError());

        [HttpPost("register")]
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
        public async Task<IActionResult> UpdateUserAsync([FromRoute] Guid userId, [FromBody] UpdateUserDto updateDto)
        {
            var updateRequest = new UpdateUserRequest(userId, AuthorizationToken);
            _mapper.Map(updateDto, updateRequest);
            
            return await _mediator
                .Send(updateRequest)
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
        [Authorize]
        public Task<IActionResult> DeleteUserAsync([FromRoute] Guid userId) =>
            _mediator
                .Send(new DeleteUserRequest(userId, AuthorizationToken))
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
