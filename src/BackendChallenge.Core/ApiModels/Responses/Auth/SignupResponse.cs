using System.Text.Json.Serialization;

namespace BackendChallenge.Core.ApiModels.Responses.Auth
{
    public class SignupResponse
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("token")]
        public string? Token { get; set; }
    }
}
