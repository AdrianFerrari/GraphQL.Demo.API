using GraphQL.Demo.API.Contracts;
using GraphQL.Demo.API.GraphQL.Types;
using GraphQL.Types;

namespace GraphQL.Demo.API.GraphQL.Queries
{
    public class AppQuery : ObjectGraphType
    {
        public AppQuery(IOwnerRepository ownerRepository, IAccountRepository accountRepository)
        {
            Field<ListGraphType<OwnerType>>("owners")
               .ResolveAsync(async context => await ownerRepository.GetAllAsync());
            Field<OwnerType>("owner")
                .Arguments(new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" }
                ))
                .ResolveAsync(async context =>
                {
                    var id = context.GetArgument<Guid>("id");
                    var owner = await ownerRepository.GetByIdAsync(id);
                    return owner ?? throw new ExecutionError("Owner not found");
                });
            Field<ListGraphType<AccountType>>("accounts")
                .Arguments(new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "ownerId" }
                ))
                .ResolveAsync(async context =>
                {
                    var ownerId = context.GetArgument<Guid>("ownerId");
                    return await accountRepository.GetAllByOwnerAsync(ownerId);
                });
            Field<AccountType>("account")
                .Arguments(new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" }
                ))
                .ResolveAsync(async context =>
                {
                    var id = context.GetArgument<Guid>("id");
                    var account = await accountRepository.GetByIdAsync(id);
                    return account ?? throw new ExecutionError("Account not found");
                });
        }
    }
}
