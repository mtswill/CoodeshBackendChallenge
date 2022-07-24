using System.Text.Json.Serialization;

namespace BackendChallenge.Core.ApiModels.Responses
{
    public class ApiErrorResponse
    {
        public ApiErrorResponse(string? message)
        {
            Message = message;
        }

        [JsonPropertyName("message")]
        public string? Message { get; set; }
    }
}
