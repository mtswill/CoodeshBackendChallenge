using BackendChallenge.Core.ApiModels.DTOs.Pagination;
using BackendChallenge.Core.ApiModels.FreeDictionary;
using BackendChallenge.Core.Result;

namespace BackendChallenge.Core.Interfaces.Services
{
    public interface IEntryService
    {
        Task<Result<List<FreeDictionaryResponse>>> GetWordFromApiAsync(string word);
        Task<Result<string>> SearchWordAsync(string word, PaginationInput paginationInput);
        Task<Result<object>> SaveFavoriteWordAsync(string word);
        Task<Result<object>> UnfavoriteWordAsync(string word);
    }
}
