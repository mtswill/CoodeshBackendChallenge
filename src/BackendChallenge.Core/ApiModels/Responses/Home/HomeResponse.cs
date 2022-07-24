namespace BackendChallenge.Core.ApiModels.Responses.Home
{
    public class HomeResponse
    {
        public HomeResponse(string? message)
        {
            Message = message;
        }

        public string? Message { get; set; }
    }
}
