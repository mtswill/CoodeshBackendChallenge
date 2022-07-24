using BackendChallenge.Core.ApiModels.DTOs.Pagination;
using BackendChallenge.Core.Entities;
using BackendChallenge.Core.Entities.Pagination;

namespace BackendChallenge.Core.Interfaces.Repositories
{
    public interface IUserHistoryRepository
    {
        Task<bool> AddHistoryAsync(UserHistory userHistory);
        Task<PaginationResult<UserHistory>?> GetUserHistoryAsync(Guid userId, PaginationInput paginationInput);
    }
}
