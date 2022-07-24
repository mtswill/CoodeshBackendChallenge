namespace BackendChallenge.Core.Entities.Pagination
{
    public class PaginationResult<T>
    {
        public IEnumerable<T>? Results { get; set; }
        public bool HasNext { get; set; }
        public bool HasPrev { get; set; }
        public int TotalDocs { get; set; }
        public int Page { get; set; }
        public int TotalPages { get; set; }
    }
}
