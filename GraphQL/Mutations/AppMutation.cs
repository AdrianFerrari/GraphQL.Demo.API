using GraphQL.Demo.API.Contracts;
using GraphQL.Demo.API.GraphQL.Requests;
using GraphQL.Demo.API.GraphQL.Types;
using GraphQL.Types;

namespace GraphQL.Demo.API.GraphQL.Mutations
{
    public class AppMutation : ObjectGraphType
    {
        public AppMutation(IOwnerRepository ownerRepository, IAccountRepository accountRepository)
        {
            Field<OwnerType>("createOwner")
                .Arguments(new QueryArguments(
                    new QueryArgument<NonNullGraphType<OwnerInputType>> { Name = "owner" }
                ))
                .ResolveAsync(async context =>
                {
                    var owner = context.GetArgument<OwnerInput>("owner");
                    var ownerdb = await ownerRepository.AddAsync(owner);
                    return ownerdb;
                });
            Field<OwnerType>("updateOwner")
                .Arguments(new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" },
                    new QueryArgument<NonNullGraphType<OwnerInputType>> { Name = "owner" }
                ))
                .ResolveAsync(async context =>
                {
                    var ownerId = context.GetArgument<Guid>("id");
                    var ownerInput = context.GetArgument<OwnerInput>("owner");
                    var owner = await ownerRepository.UpdateAsync(ownerId, ownerInput);
                    return owner;
                });
            Field<BooleanGraphType>("deleteOwner")
                .Arguments(new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" }
                ))
                .ResolveAsync(async context =>
                {
                    var id = context.GetArgument<Guid>("id");
                    await ownerRepository.DeleteAsync(id);
                    return true;
                });
            Field<AccountType>("createAccount")
                .Arguments(new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" },
                    new QueryArgument<NonNullGraphType<AccountInputType>> { Name = "account" }
                ))
                .ResolveAsync(async context =>
                {
                    var ownerId = context.GetArgument<Guid>("id");
                    var account = context.GetArgument<AccountInput>("account");
                    var newAccount = await accountRepository.AddAsync(ownerId, account);
                    return newAccount;
                });
            Field<AccountType>("updateAccount")
                .Arguments(new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" },
                    new QueryArgument<NonNullGraphType<AccountInputType>> { Name = "account" }
                ))
                .ResolveAsync(async context =>
                {
                    var id = context.GetArgument<Guid>("id");
                    var account = context.GetArgument<AccountInput>("account");
                    var newAccount = await accountRepository.UpdateAsync(id, account);
                    return newAccount;
                });
            Field<BooleanGraphType>("deleteAccount")
                .Arguments(new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" }
                ))
                .ResolveAsync(async context =>
                {
                    var id = context.GetArgument<Guid>("id");
                    var result = await accountRepository.DeleteAsync(id);
                    return result;
                });
            Field<ListGraphType<AccountType>>("accountsByOwner")
                .Arguments(new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "ownerId" }
                ))
                .ResolveAsync(async context =>
                {
                    var ownerId = context.GetArgument<Guid>("ownerId");
                    var accounts = await accountRepository.GetAllByOwnerAsync(ownerId);
                    return accounts;
                });

        }
    }
}
