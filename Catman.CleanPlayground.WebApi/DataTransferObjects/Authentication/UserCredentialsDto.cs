namespace Catman.CleanPlayground.WebApi.DataTransferObjects.Authentication
{
    using System.Text.Json.Serialization;
    using Catman.CleanPlayground.Application.Extensions.Validation;
    using Catman.CleanPlayground.Application.Localization;
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
        public UserCredentialsDtoValidator(IValidationLocalizer localizer)
        {
            RuleFor(dto => dto.Username).Required(localizer);
            RuleFor(dto => dto.Password).Required(localizer);
        }
    }
}
