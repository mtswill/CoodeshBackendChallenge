using BackendChallenge.Core.ApiModels.DTOs.Pagination;
using BackendChallenge.Core.ApiModels.Responses.User;
using BackendChallenge.Core.Result;

namespace BackendChallenge.Core.Interfaces.Services
{
    public interface IUserService
    {
        Result<UserResponse> GetMe();
        Task<Result<UserFavoriteWordsResponse>> GetFavoriteWordsAsync(PaginationInput paginationInput);
        Task<Result<UserHistoryResponse>> GetGetUserHistoryAsync(PaginationInput paginationInput);
    }
}
