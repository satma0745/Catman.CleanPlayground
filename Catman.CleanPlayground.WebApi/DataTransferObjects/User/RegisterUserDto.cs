namespace Catman.CleanPlayground.WebApi.DataTransferObjects.User
{
    using System.Text.Json.Serialization;
    using Catman.CleanPlayground.Application.Extensions.Validation;
    using FluentValidation;

    public class RegisterUserDto
    {
        [JsonPropertyName("username")]
        public string Username { get; set; }
        
        [JsonPropertyName("password")]
        public string Password { get; set; }
        
        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; }
    }

    internal class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
    {
        public RegisterUserDtoValidator()
        {
            RuleFor(dto => dto.Username).ValidUsername();
            RuleFor(dto => dto.Password).ValidPassword();
            RuleFor(dto => dto.DisplayName).ValidDisplayName();
        }
    }
}
