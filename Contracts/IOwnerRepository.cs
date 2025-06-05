using GraphQL.Demo.API.Entities;
using GraphQL.Demo.API.GraphQL.Requests;

namespace GraphQL.Demo.API.Contracts
{
    public interface IOwnerRepository
    {
        Task<IEnumerable<Owner>> GetAllAsync();
        Task<Owner?> GetByIdAsync(Guid id);
        Task<Owner> AddAsync(OwnerInput owner);
        Task<Owner> UpdateAsync(Guid id, OwnerInput owner);
        Task<bool> DeleteAsync(Guid id);
    }
}
