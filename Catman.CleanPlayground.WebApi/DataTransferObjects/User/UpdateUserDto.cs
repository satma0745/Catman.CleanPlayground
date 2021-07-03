namespace Catman.CleanPlayground.WebApi.DataTransferObjects.User
{
    using System.Text.Json.Serialization;
    using Catman.CleanPlayground.Application.Extensions.Validation;
    using FluentValidation;

    public class UpdateUserDto
    {
        [JsonPropertyName("username")]
        public string Username { get; set; }
        
        [JsonPropertyName("password")]
        public string Password { get; set; }
        
        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; }
        
        [JsonPropertyName("auth_token")]
        public string AuthenticationToken { get; set; }
    }

    internal class UpdateUserDtoValidator : AbstractValidator<UpdateUserDto>
    {
        public UpdateUserDtoValidator()
        {
            RuleFor(dto => dto.Username).ValidUsername();
            RuleFor(dto => dto.Password).ValidPassword();
            RuleFor(dto => dto.DisplayName).ValidDisplayName();
            RuleFor(dto => dto.AuthenticationToken).NotEmpty();
        }
    }
}
