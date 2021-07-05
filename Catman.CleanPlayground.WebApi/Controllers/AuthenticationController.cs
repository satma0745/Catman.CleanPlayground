namespace Catman.CleanPlayground.WebApi.Controllers
{
    using System.Threading.Tasks;
    using AutoMapper;
    using Catman.CleanPlayground.Application.Extensions.Validation;
    using Catman.CleanPlayground.Application.Services.Authentication;
    using Catman.CleanPlayground.Application.Services.Authentication.Requests;
    using Catman.CleanPlayground.Application.Services.Common.Response.Errors;
    using Catman.CleanPlayground.WebApi.DataTransferObjects.Authentication;
    using Catman.CleanPlayground.WebApi.Extensions.Services;
    using FluentValidation;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("/api/auth")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authService;
        private readonly IValidator<UserCredentialsDto> _credentialsDtoValidator;
        private readonly IMapper _mapper;

        public AuthenticationController(
            IAuthenticationService authService,
            IValidator<UserCredentialsDto> credentialsDtoValidator,
            IMapper mapper)
        {
            _authService = authService;
            _credentialsDtoValidator = credentialsDtoValidator;
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
                        _ => (IActionResult) StatusCode(500)
                    });

        [HttpPost]
        public async Task<IActionResult> AuthenticateUserAsync([FromBody] UserCredentialsDto credentialsDto)
        {
            var validationResult = _credentialsDtoValidator.Validate(credentialsDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.GetValidationErrors());
            }

            return await _authService
                .AuthenticateUserAsync(_mapper.Map<AuthenticateUserRequest>(credentialsDto))
                .SelectActionResultAsync(
                    Ok,
                    error => error switch
                    {
                        ValidationError validationError => BadRequest(validationError.ValidationErrors),
                        NotFoundError => NotFound(),
                        _ => StatusCode(500)
                    });
        }
    }
}
