using BackendChallenge.Core.Interfaces.Seeding;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackendChallenge.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class PopulateDatabaseController : BaseController
    {
        private readonly IDataSeeding _dataSeeding;

        public PopulateDatabaseController(IDataSeeding dataSeeding)
        {
            _dataSeeding=dataSeeding;
        }

        [HttpPost]
        public async Task<IActionResult> SeedAsync()
        {
            var result = await _dataSeeding.SeedAsync();
            return CustomResponse(result);
        }
    }
}
