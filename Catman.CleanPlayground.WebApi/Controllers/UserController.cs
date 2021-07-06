namespace Catman.CleanPlayground.WebApi.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper;
    using Catman.CleanPlayground.Application.UseCases.Common.RequestBroker;
    using Catman.CleanPlayground.Application.UseCases.Common.Response;
    using Catman.CleanPlayground.Application.UseCases.Common.Response.Errors;
    using Catman.CleanPlayground.Application.UseCases.Users.DeleteUser;
    using Catman.CleanPlayground.Application.UseCases.Users.GetUsers;
    using Catman.CleanPlayground.Application.UseCases.Users.RegisterUser;
    using Catman.CleanPlayground.Application.UseCases.Users.UpdateUser;
    using Catman.CleanPlayground.WebApi.DataTransferObjects.User;
    using Catman.CleanPlayground.WebApi.Extensions.UseCases;
    using Microsoft.AspNetCore.Mvc;

    [Route("/api/users")]
    public class UserController : ApiControllerBase
    {
        private readonly IRequestBroker _requestBroker;
        private readonly IMapper _mapper;

        public UserController(IRequestBroker requestBroker, IMapper mapper)
        {
            _requestBroker = requestBroker;
            _mapper = mapper;
        }

        [HttpGet]
        public Task<IActionResult> GetUsersAsync() =>
            _requestBroker
                .SendRequestAsync<GetUsersRequest, ICollection<UserResource>>(new GetUsersRequest())
                .SelectActionResultAsync(
                    users => Ok(_mapper.Map<ICollection<UserDto>>(users)),
                    _ => InternalServerError());

        [HttpPost("register")]
        public Task<IActionResult> RegisterUserAsync([FromBody] RegisterUserDto registerDto) =>
            _requestBroker
                .SendRequestAsync<RegisterUserRequest, BlankResource>(_mapper.Map<RegisterUserRequest>(registerDto))
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
            
            return await _requestBroker
                .SendRequestAsync<UpdateUserRequest, BlankResource>(updateRequest)
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
            _requestBroker
                .SendRequestAsync<DeleteUserRequest, BlankResource>(new DeleteUserRequest(userId, authorization))
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
