using BackendChallenge.Core.ApiModels.DTOs.Pagination;
using BackendChallenge.Core.ApiModels.Responses.User;
using BackendChallenge.Core.Interfaces.Repositories;
using BackendChallenge.Core.ApiModels.Responses;
using BackendChallenge.Core.Interfaces.Services;
using BackendChallenge.Core.Interfaces.Users;
using BackendChallenge.Core.Result;
using AutoMapper;
using System.Text;
using System.Text.Json;
using BackendChallenge.Core.Helpers;

namespace BackendChallenge.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUser _user;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper, IUser user)
        {
            _userRepository=userRepository;
            _mapper=mapper;
            _user=user;
        }

        public async Task<Result<UserFavoriteWordsResponse>> GetFavoriteWordsAsync(PaginationInput paginationInput)
        {
            var cacheKey = BuildCacheKey(nameof(GetFavoriteWordsAsync), paginationInput);
            if (CacheHelper.TryGetCache<UserFavoriteWordsResponse>(cacheKey, out var cached))
            {
                cached.FromCache = true;
                return cached;
            }

            var result = await _userRepository.GetFavoriteWordsAsync(_user.RequestUser!.Id, paginationInput);
            var content = _mapper.Map<PaginatedApiResponse<UserFavoriteWordsResponse>>(result);

            var response = new Result<UserFavoriteWordsResponse>().Success(content);
            CacheHelper.SetCache(cacheKey, response);
            return response;
        }

        public async Task<Result<UserHistoryResponse>> GetGetUserHistoryAsync(PaginationInput paginationInput)
        {
            var cacheKey = BuildCacheKey(nameof(GetGetUserHistoryAsync), paginationInput);
            if (CacheHelper.TryGetCache<UserHistoryResponse>(cacheKey, out var cached))
            {
                cached.FromCache = true;
                return cached;
            }

            var result = await _userRepository.GetUserHistoryAsync(_user.RequestUser!.Id, paginationInput);
            var content = _mapper.Map<PaginatedApiResponse<UserHistoryResponse>>(result);

            var response = new Result<UserHistoryResponse>().Success(content);
            CacheHelper.SetCache(cacheKey, response);
            return response;
        }

        public Result<UserResponse> GetMe()
        {
            var content = _mapper.Map<UserResponse>(_user.RequestUser);
            return new Result<UserResponse>().Success(content);
        }

        private string BuildCacheKey(string method, PaginationInput paginationInput)
        {
            var bytes = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(paginationInput));
            return $"{_user.RequestUser!.Id}_{method}_{Convert.ToBase64String(bytes)}";
        }
    }
}
