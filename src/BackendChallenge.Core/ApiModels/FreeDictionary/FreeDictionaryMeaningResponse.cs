using System.Text.Json.Serialization;

namespace BackendChallenge.Core.ApiModels.FreeDictionary
{
    public class FreeDictionaryMeaningResponse
    {
        [JsonPropertyName("partOfSpeech")]
        public string? PartOfSpeech { get; set; }

        [JsonPropertyName("definitions")]
        public List<FreeDictionaryDefinitionResponse>? Definitions { get; set; }

        [JsonPropertyName("synonyms")]
        public List<string?>? Synonyms { get; set; }

        [JsonPropertyName("antonyms")]
        public List<string?>? Antonyms { get; set; }
    }
}
