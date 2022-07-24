using BackendChallenge.Core.ApiModels.FreeDictionary;
using BackendChallenge.Core.Interfaces.Repositories;
using System.Text.Json;

namespace BackendChallenge.Infrastructure.Repositories
{
    public class FreeDictionaryRepository : IFreeDictionaryRepository
    {
        private readonly HttpClient _httpClient;

        public FreeDictionaryRepository(HttpClient httpClient)
        {
            _httpClient=httpClient;
        }

        public async Task<List<FreeDictionaryResponse>?> GetWordDefinitionAsync(string word)
        {
            var response = await _httpClient.GetAsync(word);

            if (!response.IsSuccessStatusCode)
                return null;

            var content = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<List<FreeDictionaryResponse>>(content);
        }
    }
}
