using AutoMapper;
using BackendChallenge.Core.ApiModels.DTOs.Auth;
using BackendChallenge.Core.ApiModels.Responses.Auth;
using BackendChallenge.Core.Entities;
using BackendChallenge.Core.Extensions;
using BackendChallenge.Core.Interfaces.Repositories;
using BackendChallenge.Core.Interfaces.Services;
using BackendChallenge.Core.Result;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BackendChallenge.Service.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public AuthService(IUserRepository userRepository, IConfiguration configuration, IMapper mapper)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _mapper=mapper;
        }

        public async Task<Result<SigninResponse>> SigninAsync(SigninDTO signinDTO)
        {
            var user = await _userRepository.GetUserByEmailAsync(signinDTO.Email!);

            if (user is null || user.Password != signinDTO.Password!.Encrypt())
                return new Result<SigninResponse>().Error("E-mail ou senha incorretos");

            var signinResponse = _mapper.Map<SigninResponse>(user);
            signinResponse.Token = GenerateToken(user);

            var content = _mapper.Map<SigninResponse>(signinResponse);
            return new Result<SigninResponse>().Success(content);
        }

        public async Task<Result<SignupResponse>> SignupAsync(SignupDTO signupDTO)
        {
            var validateUser = await _userRepository.GetUserByEmailAsync(signupDTO.Email!);

            if (validateUser is not null)
                return new Result<SignupResponse>().Error("E-mail já cadastrado");

            signupDTO.Password = signupDTO.Password!.Encrypt();
            var user = await _userRepository.AddUserAsync(_mapper.Map<User>(signupDTO));

            if (user is null)
                return new Result<SignupResponse>().Error("Não foi possível realizar o cadastro");

            var signupResponse = _mapper.Map<SignupResponse>(user);
            signupResponse.Token = GenerateToken(user);

            var content = _mapper.Map<SignupResponse>(signupResponse);
            return new Result<SignupResponse>().Success(content);
        }

        private string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["SecretKey"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Name!),
                    new Claim(ClaimTypes.Email, user.Email!),
                    new Claim(ClaimTypes.Hash, user.Id!.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
