using BackendChallenge.Core.ApiModels.Responses.Home;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackendChallenge.API.Controllers
{
    [Route("")]
    [ApiController]
    [AllowAnonymous]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(typeof(HomeResponse), StatusCodes.Status200OK)]
        public IActionResult Get()
        {
            return Ok(new HomeResponse("Backend Challenge 🏅 - Dictionary"));
        }
    }
}
