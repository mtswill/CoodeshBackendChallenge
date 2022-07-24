using BackendChallenge.Core.ApiModels.DTOs.Pagination;
using BackendChallenge.Core.Entities;
using BackendChallenge.Core.Entities.Pagination;

namespace BackendChallenge.Core.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<User?> AddUserAsync(User user);
        Task<User?> GetUserByIdAsync(Guid id);
        Task<User?> GetUserByEmailAsync(string email);
        Task<FavoriteWord?> GetFavoriteWordAsync(string word, Guid userId);
        Task<PaginationResult<FavoriteWord>?> GetFavoriteWordsAsync(Guid userId, PaginationInput paginationInput);
        Task<bool> SaveFavoriteWordAsync(FavoriteWord favoriteWord);
        Task<bool> UnfavoriteWordAsync(FavoriteWord favoriteWord);
        Task<bool> AddHistoryAsync(UserHistory userHistory);
        Task<PaginationResult<UserHistory>?> GetUserHistoryAsync(Guid userId, PaginationInput paginationInput);
    }
}
