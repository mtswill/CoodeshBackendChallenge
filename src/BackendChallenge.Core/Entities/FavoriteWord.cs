namespace BackendChallenge.Core.Entities
{
    public class FavoriteWord
    {
        public Guid UserId { get; set; }
        public string? Word { get; set; }
        public DateTime Added { get; set; }
    }
}
