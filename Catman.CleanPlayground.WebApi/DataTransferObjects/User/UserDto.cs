namespace Catman.CleanPlayground.WebApi.DataTransferObjects.User
{
    using System;
    using System.Text.Json.Serialization;

    public class UserDto
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        
        [JsonPropertyName("username")]
        public string Username { get; set; }
        
        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; }
    }
}
