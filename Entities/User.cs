using System.ComponentModel.DataAnnotations;

namespace GraphQL.Demo.API.Entities
{
    public class User
    {
        [Key]
        public required Guid Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public required string Name { get; set; }
        [EmailAddress]
        public required string Email { get; set; }
        public virtual ICollection<Account> Accounts { get; set; } = [];
    }
}
