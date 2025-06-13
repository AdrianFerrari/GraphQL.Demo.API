using GraphQL.Demo.API.Contracts;
using GraphQL.Demo.API.Entities;
using GraphQL.Demo.API.Entities.Context;
using GraphQL.Demo.API.GraphQL.Requests;
using Microsoft.EntityFrameworkCore;

namespace GraphQL.Demo.API.Repository
{
    public class AccountRepository(IDbContextFactory<ApplicationContext> contextFactory) : IAccountRepository
    {
        public async Task<IEnumerable<Account>> GetAllByOwnerAsync(Guid id, TypeOfAccount? typeOfAccount = null)
        {
            using var context = contextFactory.CreateDbContext();
            var query = context.Accounts.Where(x => x.UserId == id);
            if (typeOfAccount is not null)
            {
                query = query.Where(x => x.Type == typeOfAccount);
            }
            var accounts = await query.Include(a => a.User).ToListAsync();
            return accounts;
        }
        public async Task<ILookup<Guid, Account>> GetAllByOwnersAsync(IEnumerable<Guid> ids, TypeOfAccount? typeOfAccount = null)
        {
            using var context = contextFactory.CreateDbContext();
            var query = context.Accounts
                .Where(x => ids.Contains(x.UserId));

            if (typeOfAccount.HasValue)
            {
                query = query.Where(x => x.Type == typeOfAccount.Value);
            }
            var accounts = await query.Include(a => a.User).ToListAsync();
            return accounts.ToLookup(x => x.UserId);
        }
        public async Task<Account?> GetByIdAsync(Guid id)
        {
            using var context = contextFactory.CreateDbContext();
            return await context.Accounts
                .Include(a => a.User)
                .FirstOrDefaultAsync(a => a.Id == id);
        }
        public async Task<Account> AddAsync(Guid ownerId, AccountInput request)
        {
            using var context = contextFactory.CreateDbContext();
            var owner = await context.Users.FindAsync(ownerId);
            if (owner == null) throw new InvalidOperationException("Owner not found");
            var account = new Account
            {
                Id = Guid.NewGuid(),
                User = owner,
                Type = request.Type,
                Description = request.Description
            };
            await context.Accounts.AddAsync(account);
            await context.SaveChangesAsync();
            return account;
        }
        public async Task<Account> UpdateAsync(Guid id, AccountInput input)
        {
            using var context = contextFactory.CreateDbContext();
            var account = await context.Accounts.FindAsync(id);
            if (account == null) throw new InvalidOperationException("Account not found");
            account.Type = input.Type;
            account.Description = input.Description;
            await context.SaveChangesAsync();
            return account;
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            using var context = contextFactory.CreateDbContext();
            var account = await context.Accounts.FindAsync(id);
            if (account == null) return false;
            context.Accounts.Remove(account);
            await context.SaveChangesAsync();
            return true;
        }
    }
}
