using BackendChallenge.Core.ApiModels.FreeDictionary;

namespace BackendChallenge.Core.Interfaces.Repositories
{
    public interface IFreeDictionaryRepository
    {
        Task<List<FreeDictionaryResponse>?> GetWordDefinitionAsync(string word);
    }
}
