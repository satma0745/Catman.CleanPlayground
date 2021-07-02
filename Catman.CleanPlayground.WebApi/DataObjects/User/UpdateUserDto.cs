namespace Catman.CleanPlayground.WebApi.DataObjects.User
{
    using System.Text.Json.Serialization;

    public class UpdateUserDto
    {
        [JsonPropertyName("username")]
        public string Username { get; set; }
        
        [JsonPropertyName("password")]
        public string Password { get; set; }
        
        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; }
    }
}
