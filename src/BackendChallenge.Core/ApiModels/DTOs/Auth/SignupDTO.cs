using BackendChallenge.Core.Result;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BackendChallenge.Core.ApiModels.DTOs.Auth
{
    public class SignupDTO
    {
        [JsonPropertyName("name")]
        [Required]
        public string? Name { get; set; }

        [JsonPropertyName("email")]
        [Required]
        [EmailAddress(ErrorMessage = "E-mail inválido")]
        public string? Email { get; set; }

        [JsonPropertyName("password")]
        [Required]
        public string? Password { get; set; }

        public Result<object> Validate()
        {
            var errorMessage = string.Empty;

            if (Name!.Length < 3 || Name.Length > 50)
                errorMessage += "Nome deve ter entre 3 e 50 caracteres; ";

            if (Email!.Length < 3 || Email.Length > 50)
                errorMessage += "E-mail deve ter entre 3 e 50 caracteres; ";

            if (Password!.Length < 3 || Password.Length > 50)
                errorMessage += "Senha deve ter entre 3 e 20 caracteres";

            if (!string.IsNullOrWhiteSpace(errorMessage))
                return new Result<object>().Error(errorMessage);

            return new Result<object>().Success();
        }
    }
}
