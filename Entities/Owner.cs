using System.ComponentModel.DataAnnotations;

namespace GraphQL.Demo.API.Entities
{
    public class Owner
    {
        [Key]
        public required Guid Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public required string Name { get; set; }
        public string? Address { get; set; }
        public virtual ICollection<Account> Accounts { get; set; } = [];
    }

    //public class Owner(ILazyLoader lazyLoader)
    //{
    //    [Key]
    //    public required Guid Id { get; set; }
    //    [Required(ErrorMessage = "Name is required")]
    //    public required string Name { get; set; }
    //    public string? Address { get; set; }
    //    private ICollection<Account> _accounts = [];
    //    public ICollection<Account> Accounts
    //    {
    //        get => lazyLoader.Load(this, ref _accounts) ?? [];
    //        set => _accounts = value ?? [];
    //    }
    //}
}
