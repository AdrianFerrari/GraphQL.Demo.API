using Microsoft.EntityFrameworkCore;

namespace GraphQL.Demo.API.Entities.Context
{
    public class ApplicationContext(DbContextOptions options) : DbContext(options)
    {
        public virtual required DbSet<Owner> Owners { get; set; }
        public virtual required DbSet<Account> Accounts { get; set; }
    }
}
