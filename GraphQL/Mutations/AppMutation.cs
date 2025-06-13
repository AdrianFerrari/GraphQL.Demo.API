using GraphQL.Demo.API.Contracts;
using GraphQL.Demo.API.GraphQL.Requests;
using GraphQL.Demo.API.GraphQL.Types;
using GraphQL.Types;

namespace GraphQL.Demo.API.GraphQL.Mutations
{
    public class AppMutation : ObjectGraphType
    {
        public AppMutation(IUserRepository userRepository, IAccountRepository accountRepository)
        {
            Field<UserType>("createUser")
                .Arguments(new QueryArguments(
                    new QueryArgument<NonNullGraphType<UserInputType>> { Name = "user" }
                ))
                .ResolveAsync(async context =>
                {
                    var userInput = context.GetArgument<UserInput>("user");
                    var userDb = await userRepository.AddAsync(userInput);
                    return userDb;
                });
            Field<UserType>("updateUser")
                .Arguments(new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" },
                    new QueryArgument<NonNullGraphType<UserInputType>> { Name = "user" }
                ))
                .ResolveAsync(async context =>
                {
                    var userId = context.GetArgument<Guid>("id");
                    var userInput = context.GetArgument<UserInput>("user");
                    var user = await userRepository.UpdateAsync(userId, userInput);
                    return user;
                });
            Field<BooleanGraphType>("deleteUser")
                .Arguments(new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" }
                ))
                .ResolveAsync(async context =>
                {
                    var id = context.GetArgument<Guid>("id");
                    await userRepository.DeleteAsync(id);
                    return true;
                });
            Field<AccountType>("createAccount")
                .Arguments(new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" },
                    new QueryArgument<NonNullGraphType<AccountInputType>> { Name = "account" }
                ))
                .ResolveAsync(async context =>
                {
                    var userId = context.GetArgument<Guid>("id");
                    var account = context.GetArgument<AccountInput>("account");
                    var newAccount = await accountRepository.AddAsync(userId, account);
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
