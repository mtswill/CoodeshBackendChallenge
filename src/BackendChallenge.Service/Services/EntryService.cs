using AutoMapper;
using BackendChallenge.Core.ApiModels.DTOs.Pagination;
using BackendChallenge.Core.ApiModels.FreeDictionary;
using BackendChallenge.Core.ApiModels.Responses;
using BackendChallenge.Core.Entities;
using BackendChallenge.Core.Helpers;
using BackendChallenge.Core.Interfaces.Repositories;
using BackendChallenge.Core.Interfaces.Services;
using BackendChallenge.Core.Interfaces.Users;
using BackendChallenge.Core.Result;
using System.Text;
using System.Text.Json;

namespace BackendChallenge.Service.Services
{
    public class EntryService : IEntryService
    {
        private readonly IFreeDictionaryRepository _freeDictionaryRepository;
        private readonly IEntryRepository _entryRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserHistoryRepository _userHistoryRepository;
        private readonly IMapper _mapper;
        private readonly IUser _user;

        public EntryService(IFreeDictionaryRepository freeDictionaryRepository, IUser user, IUserRepository userRepository, IEntryRepository entryRepository, IMapper mapper, IUserHistoryRepository userHistoryRepository)
        {
            _freeDictionaryRepository = freeDictionaryRepository;
            _user = user;
            _userRepository = userRepository;
            _entryRepository = entryRepository;
            _mapper = mapper;
            _userHistoryRepository = userHistoryRepository;
        }

        public async Task<Result<object>> SaveFavoriteWordAsync(string word)
        {
            var favoriteWord = await _userRepository.GetFavoriteWordAsync(word, _user.RequestUser!.Id);

            if (favoriteWord is not null)
                return new Result<object>().Error("Palavra já é favorita");

            var result = await _userRepository.SaveFavoriteWordAsync(new FavoriteWord
            {
                UserId = _user.RequestUser.Id,
                Word = word,
                Added = DateTime.UtcNow
            });

            if (!result)
                return new Result<object>().Error("Não foi possível realizar a operação");

            return new Result<object>().Success();
        }

        public async Task<Result<List<FreeDictionaryResponse>>> GetWordFromApiAsync(string word)
        {
            await SaveHistoryAsync(word);

            var cacheKey = BuildCacheKey(word, nameof(GetWordFromApiAsync));
            if (CacheHelper.TryGetCache<List<FreeDictionaryResponse>>(cacheKey, out var cached))
            {
                cached.FromCache = true;
                return cached;
            }

            var result = await _freeDictionaryRepository.GetWordDefinitionAsync(word);

            if (result is null)
                return new Result<List<FreeDictionaryResponse>>().Error("Não foi possível buscar a palavra");

            var response = new Result<List<FreeDictionaryResponse>>().Success(result);
            CacheHelper.SetCache(cacheKey, response);
            return response;
        }

        public async Task<Result<object>> UnfavoriteWordAsync(string word)
        {
            var favoriteWord = await _userRepository.GetFavoriteWordAsync(word, _user.RequestUser!.Id);

            if (favoriteWord is null)
                return new Result<object>().Error("Palavra não é favorita");

            var result = await _userRepository.UnfavoriteWordAsync(favoriteWord);

            if (!result)
                return new Result<object>().Error("Não foi possível realizar a operação");

            return new Result<object>().Success();
        }

        public async Task<Result<string>> SearchWordAsync(string word, PaginationInput paginationInput)
        {
            var cacheKey = BuildCacheKey(word, nameof(GetWordFromApiAsync), paginationInput);
            if (CacheHelper.TryGetCache<string>(cacheKey, out var cached))
            {
                cached.FromCache = true;
                return cached;
            }

            var searchResult = await _entryRepository.SearchWordAsync(word, paginationInput);
            var content = _mapper.Map<PaginatedApiResponse<string>>(searchResult);

            var response = new Result<string>().Success(content);
            CacheHelper.SetCache(cacheKey, response);
            return response;
        }

        private async Task SaveHistoryAsync(string word)
        {
            var userHistory = new UserHistory
            {
                Added = DateTime.UtcNow,
                UserId = _user.RequestUser!.Id,
                Word = word
            };

            await _userHistoryRepository.AddHistoryAsync(userHistory);
        }

        private string BuildCacheKey(string word, string method, PaginationInput? paginationInput = null)
        {
            var cacheKey = $"{_user.RequestUser!.Id}_{word}_{method}";

            if (paginationInput is not null)
            {
                var bytes = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(paginationInput));
                cacheKey += $"_{Convert.ToBase64String(bytes)}";
            }

            return cacheKey;
        }
    }
}
