using System.ComponentModel.DataAnnotations;

namespace BackendChallenge.Core.Entities
{
    public class Word
    {
        [Key]
        public string? Text { get; set; }
    }
}
