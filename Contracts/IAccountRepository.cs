using GraphQL.Demo.API.Entities;
using GraphQL.Demo.API.GraphQL.Requests;

namespace GraphQL.Demo.API.Contracts
{
    public interface IAccountRepository
    {
        Task<IEnumerable<Account>> GetAllByOwnerAsync(Guid ownerId, TypeOfAccount? typeOfAccount = null);
        Task<ILookup<Guid, Account>> GetAllByOwnersAsync(IEnumerable<Guid> ids, TypeOfAccount? typeOfAccount = null);
        Task<Account?> GetByIdAsync(Guid id);
        Task<Account> AddAsync(Guid ownerId, AccountInput account);
        Task<Account> UpdateAsync(Guid id, AccountInput account);
        Task<bool> DeleteAsync(Guid id);
    }
}
