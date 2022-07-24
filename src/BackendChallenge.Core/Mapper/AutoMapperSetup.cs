using AutoMapper;
using BackendChallenge.Core.ApiModels.DTOs.Auth;
using BackendChallenge.Core.ApiModels.Responses;
using BackendChallenge.Core.ApiModels.Responses.Auth;
using BackendChallenge.Core.ApiModels.Responses.User;
using BackendChallenge.Core.Entities;
using BackendChallenge.Core.Entities.Pagination;

namespace BackendChallenge.Core.Mapper
{
    public class AutoMapperSetup : Profile
    {
        public AutoMapperSetup()
        {
            MapEntities();
            MapResponses();
        }

        private void MapEntities()
        {
            CreateMap<SignupDTO, User>();
        }

        private void MapResponses()
        {
            CreateMap<User, SignupResponse>();
            CreateMap<User, UserResponse>();
            CreateMap<User, SigninResponse>();
            CreateMap<FavoriteWord, UserFavoriteWordsResponse>();
            CreateMap<UserHistory, UserHistoryResponse>();

            CreateMap(typeof(PaginationResult<>), typeof(PaginatedApiResponse<>));
        }
    }
}
