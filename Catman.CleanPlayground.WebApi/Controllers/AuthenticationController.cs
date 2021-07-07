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

        [HttpGet]
        public Task<IActionResult> GetCurrentUserAsync([FromHeader] string authorization) =>
            _mediator
                .Send(new GetCurrentUserRequest(authorization))
                .SelectActionResultAsync(
                    user => Ok(_mapper.Map<CurrentUserDto>(user)),
                    error => error switch
                    {
                        AuthenticationError => Unauthorized(),
                        _ => InternalServerError()
                    });

        [HttpPost]
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
