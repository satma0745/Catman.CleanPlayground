namespace Catman.CleanPlayground.WebApi.DataTransferObjects.User
{
    using System.Text.Json.Serialization;
    using FluentValidation;

    public class DeleteUserDto
    {
        [JsonPropertyName("auth_token")]
        public string AuthenticationToken { get; set; }
    }

    internal class DeleteUserDtoValidator : AbstractValidator<DeleteUserDto>
    {
        public DeleteUserDtoValidator()
        {
            RuleFor(dto => dto.AuthenticationToken).NotEmpty();
        }
    }
}
