using BackendChallenge.Core.ApiModels.DTOs.Pagination;
using BackendChallenge.Core.ApiModels.Responses;
using BackendChallenge.Core.ApiModels.Responses.User;
using BackendChallenge.Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackendChallenge.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("me")]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        public IActionResult Me()
        {
            var response = _userService.GetMe();
            return CustomResponse(response);
        }

        [HttpGet("me/history")]
        [ProducesResponseType(typeof(PaginatedApiResponse<UserHistoryResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> HistoryAsync([FromQuery] PaginationInput paginationInput)
        {
            var response = await _userService.GetGetUserHistoryAsync(paginationInput);
            return CustomResponse(response);
        }

        [HttpGet("me/favorites")]
        [ProducesResponseType(typeof(PaginatedApiResponse<UserFavoriteWordsResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> FavoritesAsync([FromQuery] PaginationInput paginationInput)
        {
            var response = await _userService.GetFavoriteWordsAsync(paginationInput);
            return CustomResponse(response);
        }
    }
}
