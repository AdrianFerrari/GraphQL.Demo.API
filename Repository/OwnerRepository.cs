using GraphQL.Demo.API.Contracts;
using GraphQL.Demo.API.Entities;
using GraphQL.Demo.API.Entities.Context;
using GraphQL.Demo.API.GraphQL.Requests;
using Microsoft.EntityFrameworkCore;

namespace GraphQL.Demo.API.Repository
{
    public class OwnerRepository(IDbContextFactory<ApplicationContext> contextFactory) : IOwnerRepository
    {
        public async Task<IEnumerable<Owner>> GetAllAsync()
        {
            using var context = contextFactory.CreateDbContext();
            return await context.Owners.ToListAsync();
        }
        public async Task<Owner?> GetByIdAsync(Guid id)
        {
            using var context = contextFactory.CreateDbContext();
            return await context.Owners.FirstOrDefaultAsync(o => o.Id == id);
        }
        public async Task<Owner> AddAsync(OwnerInput request)
        {
            using var context = contextFactory.CreateDbContext();
            var owner = new Owner
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Address = request.Address
            };
            context.Owners.Add(owner);
            await context.SaveChangesAsync();
            return owner;
        }
        public async Task<Owner> UpdateAsync(Guid id, OwnerInput input)
        {
            using var context = contextFactory.CreateDbContext();
            var dbOwner = await context.Owners.FindAsync(id);
            if (dbOwner == null) throw new InvalidOperationException("Owner not found");
            dbOwner.Name = input.Name ?? dbOwner.Name;
            dbOwner.Address = input.Address ?? dbOwner.Address;
            await context.SaveChangesAsync();
            return dbOwner;
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            using var context = contextFactory.CreateDbContext();
            var owner = await context.Owners.FindAsync(id);
            if (owner == null) return false;
            context.Owners.Remove(owner);
            await context.SaveChangesAsync();
            return true;
        }
    }
}
