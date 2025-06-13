using GraphQL.Demo.API.Entities;
using GraphQL.Demo.API.GraphQL.Requests;

namespace GraphQL.Demo.API.Contracts
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User?> GetByIdAsync(Guid id);
        Task<User> AddAsync(UserInput owner);
        Task<User> UpdateAsync(Guid id, UserInput owner);
        Task<bool> DeleteAsync(Guid id);
    }
}
