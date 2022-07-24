using System.Text.Json.Serialization;

namespace BackendChallenge.Core.ApiModels.Responses
{
    public class PaginatedApiResponse<T>
    {
        [JsonPropertyName("results")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public IEnumerable<T>? Results { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("totalDocs")]
        public int? TotalDocs { get; set; }

        [JsonPropertyName("page")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? Page { get; set; }

        [JsonPropertyName("totalPages")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? TotalPages { get; set; }

        [JsonPropertyName("hasNext")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? HasNext { get; set; }

        [JsonPropertyName("hasPrev")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? HasPrev { get; set; }
    }
}
