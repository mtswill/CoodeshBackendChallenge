using BackendChallenge.Core.ApiModels.DTOs.Auth;
using BackendChallenge.Core.ApiModels.Responses;
using BackendChallenge.Core.ApiModels.Responses.Auth;
using BackendChallenge.Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackendChallenge.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : BaseController
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("signup")]
        [ProducesResponseType(typeof(SignupResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SignupAsync(SignupDTO signupDTO)
        {
            var validate = signupDTO.Validate();

            if (!validate.IsSuccess)
                return CustomResponse(validate);

            var signupResponse = await _authService.SignupAsync(signupDTO);
            return CustomResponse(signupResponse);
        }

        [HttpPost("signin")]
        [ProducesResponseType(typeof(SigninResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SigninAsync(SigninDTO signinDTO)
        {
            var validate = signinDTO.Validate();

            if (!validate.IsSuccess)
                return CustomResponse(validate);

            var signinResponse = await _authService.SigninAsync(signinDTO);
            return CustomResponse(signinResponse);
        }
    }
}
