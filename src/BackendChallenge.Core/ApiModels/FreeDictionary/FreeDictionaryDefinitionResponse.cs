using System.Text.Json.Serialization;

namespace BackendChallenge.Core.ApiModels.FreeDictionary
{
    public class FreeDictionaryDefinitionResponse
    {
        [JsonPropertyName("definition")]
        public string? Definition { get; set; }

        [JsonPropertyName("synonyms")]
        public List<string?>? Synonyms { get; set; }

        [JsonPropertyName("antonyms")]
        public List<string?>? Antonyms { get; set; }

        [JsonPropertyName("example")]
        public string? Example { get; set; }
    }
}
