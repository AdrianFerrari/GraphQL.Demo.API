using Microsoft.EntityFrameworkCore;

namespace GraphQL.Demo.API.Entities.Context
{
    public class ApplicationContext(DbContextOptions options) : DbContext(options)
    {
        public virtual required DbSet<User> Users { get; set; }
        public virtual required DbSet<Account> Accounts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSeeding((context, _) =>
            {
                var bobId = new Guid("d1482d45-dc2b-4629-8a4d-1832ad28e5b1");
                var aliceId = new Guid("b1c8f3d2-4e5a-4c6b-9f7d-8e2f3a4b5c6d");
                if (!context.Set<User>().Any(u => u.Id == bobId))
                {
                    var bob = new User { Id = bobId, Name = "Bob", Email = "bob@email.com" };
                    context.Set<Account>().AddRange(
                        new Account { Id = Guid.NewGuid(), Type = TypeOfAccount.Income, Description = "Food delivery job", User = bob },
                        new Account { Id = Guid.NewGuid(), Type = TypeOfAccount.Income, Description = "Freelance job", User = bob },
                        new Account { Id = Guid.NewGuid(), Type = TypeOfAccount.Expense, Description = "Rent", User = bob }
                    );
                }
                if (!context.Set<User>().Any(u => u.Id == aliceId))
                {
                    var alice = new User { Id = aliceId, Name = "Alice", Email = "alice@email.com" };
                    context.Set<Account>().AddRange(
                        new Account { Id = Guid.NewGuid(), Type = TypeOfAccount.Savings, Description = "New bike savings", User = alice },
                        new Account { Id = Guid.NewGuid(), Type = TypeOfAccount.Expense, Description = "Groceries", User = alice }
                    );
                }
                context.SaveChanges();
            });
        }
    }
}
