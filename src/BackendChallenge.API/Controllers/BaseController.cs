using BackendChallenge.Core.Result;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BackendChallenge.API.Controllers
{
    public class BaseController : ControllerBase
    {
        protected IActionResult CustomResponse<T>(Result<T> result)
        {
            AddHeaders(result);

            if (result.IsSuccess)
            {
                if (result.SimpleResponse is not null)
                    return Ok(result.SimpleResponse);

                return result.PaginatedApiResponse is null ? Ok() : Ok(result.PaginatedApiResponse);
            }

            return result.ApiError is null ? BadRequest() : BadRequest(result.ApiError);
        }

        private void AddHeaders<T>(Result<T> result)
        {
            HttpContext.Response.Headers.Add("X-Cache", result.FromCache ? "HIT" : "MISS");

            if (HttpContext.Items.TryGetValue("stopwatch", out var content))
            {
                var stopwatch = (Stopwatch)content!;
                stopwatch.Stop();

                HttpContext.Response.Headers.Add("X-Response-Time", stopwatch.ElapsedMilliseconds.ToString());
            }
        }
    }
}
