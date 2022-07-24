using System.Text.Json.Serialization;

namespace BackendChallenge.Core.ApiModels.Responses.User
{
    public class UserHistoryResponse
    {
        [JsonPropertyName("added")]
        public DateTime Added { get; set; }

        [JsonPropertyName("word")]
        public string? Word { get; set; }
    }
}
