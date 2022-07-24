using System.Text.Json.Serialization;

namespace BackendChallenge.Core.ApiModels.FreeDictionary
{
    public class FreeDictionaryLicenseResponse
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("url")]
        public string? Url { get; set; }
    }
}
