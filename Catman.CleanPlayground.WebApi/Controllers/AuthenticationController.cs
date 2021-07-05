namespace Catman.CleanPlayground.WebApi.Controllers
{
    using System.Threading.Tasks;
    using AutoMapper;
    using Catman.CleanPlayground.Application.Services.Authentication;
    using Catman.CleanPlayground.Application.Services.Authentication.Requests;
    using Catman.CleanPlayground.Application.Services.Common.Response.Errors;
    using Catman.CleanPlayground.WebApi.DataTransferObjects.Authentication;
    using Catman.CleanPlayground.WebApi.Extensions.Services;
    using Microsoft.AspNetCore.Mvc;

    [Route("/api/auth")]
    public class AuthenticationController : ApiControllerBase
    {
        private readonly IAuthenticationService _authService;
        private readonly IMapper _mapper;

        public AuthenticationController(IAuthenticationService authService, IMapper mapper)
        {
            _authService = authService;
            _mapper = mapper;
        }

        [HttpGet]
        public Task<IActionResult> GetCurrentUserAsync([FromHeader] string authorization) =>
            _authService
                .GetCurrentUserAsync(new GetCurrentUserRequest(authorization))
                .SelectActionResultAsync(
                    user => Ok(_mapper.Map<CurrentUserDto>(user)),
                    error => error switch
                    {
                        AuthenticationError => Unauthorized(),
                        _ => InternalServerError()
                    });

        [HttpPost]
        public Task<IActionResult> AuthenticateUserAsync([FromBody] UserCredentialsDto credentialsDto) =>
            _authService
                .AuthenticateUserAsync(_mapper.Map<AuthenticateUserRequest>(credentialsDto))
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
