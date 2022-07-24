using BackendChallenge.Core.ApiModels.Responses.Auth;
using BackendChallenge.Core.ApiModels.DTOs.Auth;
using BackendChallenge.Core.Result;

namespace BackendChallenge.Core.Interfaces.Services
{
    public interface IAuthService
    {
        Task<Result<SignupResponse>> SignupAsync(SignupDTO signupDTO);
        Task<Result<SigninResponse>> SigninAsync(SigninDTO signinDTO);
    }
}
