using BackendChallenge.Core.ApiModels.DTOs.Pagination;
using BackendChallenge.Core.ApiModels.FreeDictionary;
using BackendChallenge.Core.ApiModels.Responses;
using BackendChallenge.Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackendChallenge.API.Controllers
{
    [Route("[controller]/en")]
    [ApiController]
    [Authorize]
    public class EntriesController : BaseController
    {
        private readonly IEntryService _entryService;

        public EntriesController(IEntryService entryService)
        {
            _entryService=entryService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(PaginatedApiResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SearchWordAsync([FromQuery] string? search, [FromQuery] PaginationInput paginationInput)
        {
            var result = await _entryService.SearchWordAsync(search!, paginationInput);
            return CustomResponse(result);
        }

        [HttpGet("{word}")]
        [ProducesResponseType(typeof(List<FreeDictionaryResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetWordDefinitionAsync(string word)
        {
            if (string.IsNullOrWhiteSpace(word))
                return BadRequest();

            var result = await _entryService.GetWordFromApiAsync(word);
            return CustomResponse(result);
        }

        [HttpPost("{word}/favorite")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> FavoriteWordAsync(string word)
        {
            if (string.IsNullOrWhiteSpace(word))
                return BadRequest();

            var response = await _entryService.SaveFavoriteWordAsync(word);
            return CustomResponse(response);
        }

        [HttpDelete("{word}/unfavorite")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UnfavoriteWordAsync(string word)
        {
            if (string.IsNullOrWhiteSpace(word))
                return BadRequest();

            var response = await _entryService.UnfavoriteWordAsync(word);
            return CustomResponse(response);
        }
    }
}
