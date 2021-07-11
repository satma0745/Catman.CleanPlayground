namespace Catman.CleanPlayground.WebApi.DataTransferObjects.User
{
    using System.Text.Json.Serialization;
    using Catman.CleanPlayground.Application.Extensions.Validation;
    using Catman.CleanPlayground.Application.Localization;
    using FluentValidation;

    public class UpdateUserDto
    {
        [JsonPropertyName("username")]
        public string Username { get; set; }
        
        [JsonPropertyName("password")]
        public string Password { get; set; }
        
        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; }
    }

    internal class UpdateUserDtoValidator : AbstractValidator<UpdateUserDto>
    {
        public UpdateUserDtoValidator(IValidationLocalizer localizer)
        {
            RuleFor(dto => dto.Username).ValidUsername(localizer);
            RuleFor(dto => dto.Password).ValidPassword(localizer);
            RuleFor(dto => dto.DisplayName).ValidDisplayName(localizer);
        }
    }
}
