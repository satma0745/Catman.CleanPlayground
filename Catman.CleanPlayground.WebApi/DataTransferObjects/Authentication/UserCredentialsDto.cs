namespace Catman.CleanPlayground.WebApi.DataTransferObjects.Authentication
{
    using System.Text.Json.Serialization;
    using FluentValidation;

    public class UserCredentialsDto
    {
        [JsonPropertyName("Username")]
        public string Username { get; set; }
        
        [JsonPropertyName("Password")]
        public string Password { get; set; }
    }

    internal class UserCredentialsDtoValidator : AbstractValidator<UserCredentialsDto>
    {
        public UserCredentialsDtoValidator()
        {
            RuleFor(dto => dto.Username).NotEmpty();
            RuleFor(dto => dto.Password).NotEmpty();
        }
    }
}
