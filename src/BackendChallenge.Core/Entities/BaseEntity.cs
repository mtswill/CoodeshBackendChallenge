using System.ComponentModel.DataAnnotations;

namespace BackendChallenge.Core.Entities
{
    public abstract class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
    }
}
