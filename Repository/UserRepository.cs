using GraphQL.Demo.API.Contracts;
using GraphQL.Demo.API.Entities;
using GraphQL.Demo.API.Entities.Context;
using GraphQL.Demo.API.GraphQL.Requests;
using Microsoft.EntityFrameworkCore;

namespace GraphQL.Demo.API.Repository
{
    public class UserRepository(IDbContextFactory<ApplicationContext> contextFactory) : IUserRepository
    {
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            using var context = contextFactory.CreateDbContext();
            return await context.Users.ToListAsync();
        }
        public async Task<User?> GetByIdAsync(Guid id)
        {
            using var context = contextFactory.CreateDbContext();
            return await context.Users.FirstOrDefaultAsync(o => o.Id == id);
        }
        public async Task<User> AddAsync(UserInput request)
        {
            using var context = contextFactory.CreateDbContext();
            var owner = new User
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Email = request.Email
            };
            context.Users.Add(owner);
            await context.SaveChangesAsync();
            return owner;
        }
        public async Task<User> UpdateAsync(Guid id, UserInput input)
        {
            using var context = contextFactory.CreateDbContext();
            var dbOwner = await context.Users.FindAsync(id);
            if (dbOwner == null) throw new InvalidOperationException("Owner not found");
            dbOwner.Name = input.Name ?? dbOwner.Name;
            dbOwner.Email = input.Email ?? dbOwner.Email;
            await context.SaveChangesAsync();
            return dbOwner;
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            using var context = contextFactory.CreateDbContext();
            var owner = await context.Users.FindAsync(id);
            if (owner == null) return false;
            context.Users.Remove(owner);
            await context.SaveChangesAsync();
            return true;
        }
    }
}
