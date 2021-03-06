namespace Catman.CleanPlayground.WebApi.Controllers
{
    using System.Threading.Tasks;
    using AutoMapper;
    using Catman.CleanPlayground.Application.UseCases.Authentication.AuthenticateUser;
    using Catman.CleanPlayground.Application.UseCases.Authentication.GetCurrentUser;
    using Catman.CleanPlayground.Application.UseCases.Common.Response.Errors;
    using Catman.CleanPlayground.WebApi.DataTransferObjects.Authentication;
    using Catman.CleanPlayground.WebApi.Extensions.UseCases;
    using MediatR;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    [Route("/api/auth")]
    public class AuthenticationController : ApiControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public AuthenticationController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("who-am-i")]
        [Authorize]
        [ProducesResponseType(typeof(CurrentUserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> GetCurrentUserAsync() =>
            _mediator
                .Send(new GetCurrentUserRequest(AuthorizationToken))
                .SelectActionResultAsync(
                    user => Ok(_mapper.Map<CurrentUserDto>(user)),
                    error => error switch
                    {
                        AuthenticationError => Unauthorized(),
                        _ => InternalServerError()
                    });

        [HttpPost("token")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> AuthenticateUserAsync([FromBody] UserCredentialsDto credentialsDto) =>
            _mediator
                .Send(_mapper.Map<AuthenticateUserRequest>(credentialsDto))
                .SelectActionResultAsync(
                    Ok,
                    error => error switch
                    {
                        ValidationError validationError => BadRequest(validationError.ValidationErrors),
                        NotFoundError => NotFound(),
                        _ => InternalServerError()
                    });
    }
}
