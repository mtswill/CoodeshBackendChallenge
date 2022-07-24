using System.Text.Json.Serialization;

namespace BackendChallenge.Core.ApiModels.FreeDictionary
{
    public class FreeDictionaryResponse
    {
        [JsonPropertyName("word")]
        public string? Word { get; set; }

        [JsonPropertyName("phonetic")]
        public string? Phonetic { get; set; }

        [JsonPropertyName("phonetics")]
        public List<FreeDictionaryPhoneticResponse>? Phonetics { get; set; }

        [JsonPropertyName("meanings")]
        public List<FreeDictionaryMeaningResponse>? Meanings { get; set; }

        [JsonPropertyName("license")]
        public FreeDictionaryLicenseResponse? License { get; set; }

        [JsonPropertyName("sourceUrls")]
        public List<string?>? SourceUrls { get; set; }
    }
}
