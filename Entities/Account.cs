using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraphQL.Demo.API.Entities
{
    public class Account
    {
        [Key]
        public required Guid Id { get; set; }
        [Required(ErrorMessage = "Type is required")]
        public TypeOfAccount Type { get; set; }
        public string? Description { get; set; }
        public Guid UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual required User User { get; set; }
    }
}
