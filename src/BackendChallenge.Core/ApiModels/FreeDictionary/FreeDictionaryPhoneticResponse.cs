using System.Text.Json.Serialization;

namespace BackendChallenge.Core.ApiModels.FreeDictionary
{
    public class FreeDictionaryPhoneticResponse
    {
        [JsonPropertyName("text")]
        public string? Text { get; set; }

        [JsonPropertyName("audio")]
        public string? Audio { get; set; }

        [JsonPropertyName("sourceUrl")]
        public string? SourceUrl { get; set; }

        [JsonPropertyName("license")]
        public FreeDictionaryLicenseResponse? License { get; set; }
    }
}
