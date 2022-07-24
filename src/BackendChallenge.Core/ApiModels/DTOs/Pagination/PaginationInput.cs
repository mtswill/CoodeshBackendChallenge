namespace BackendChallenge.Core.ApiModels.DTOs.Pagination
{
    public class PaginationInput
    {
        public int Limit { get; set; }
        public int Page { get; set; } = 1;

        public bool Validate()
        {
            return Limit > 0 && Page > 0;
        }
    }
}
