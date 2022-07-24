using BackendChallenge.Core.ApiModels.DTOs.Pagination;
using BackendChallenge.Core.Entities.Pagination;

namespace BackendChallenge.Core.Interfaces.Repositories
{
    public interface IEntryRepository
    {
        Task<PaginationResult<string>> SearchWordAsync(string word, PaginationInput paginationInput);
    }
}
