namespace Catman.CleanPlayground.WebApi.Controllers
{
    using System.Threading.Tasks;
    using AutoMapper;
    using Catman.CleanPlayground.Application.Extensions.Services;
    using Catman.CleanPlayground.Application.Extensions.Validation;
    using Catman.CleanPlayground.Application.Services.Authentication;
    using Catman.CleanPlayground.Application.Services.Authentication.Requests;
    using Catman.CleanPlayground.Application.Services.Common.Response.Errors;
    using Catman.CleanPlayground.WebApi.DataTransferObjects.Authentication;
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

        [HttpPost]
        public async Task<IActionResult> AuthenticateUserAsync([FromBody] UserCredentialsDto credentialsDto)
        {
            var validationResult = await _credentialsDtoValidator.ValidateAsync(credentialsDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.GetValidationErrors());
            }
            
            var authenticateRequest = _mapper.Map<AuthenticateUserRequest>(credentialsDto);
            var operationResult = await _authService.AuthenticateUserAsync(authenticateRequest);
            
            return operationResult.Select(
                onSuccess: Ok,
                onFailure: error => error switch
                {
                    ValidationError validationError => BadRequest(validationError.ValidationErrors),
                    NotFoundError => NotFound(),
                    _ => (IActionResult) StatusCode(500)
                });
        }
    }
}
