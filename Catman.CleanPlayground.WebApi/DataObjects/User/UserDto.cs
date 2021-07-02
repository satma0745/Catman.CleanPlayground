namespace Catman.CleanPlayground.WebApi.DataObjects.User
{
    using System.Text.Json.Serialization;

    public class UserDto
    {
        [JsonPropertyName("id")]
        public byte Id { get; set; }
        
        [JsonPropertyName("username")]
        public string Username { get; set; }
        
        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; }
    }
}
