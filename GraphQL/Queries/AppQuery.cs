using GraphQL.Demo.API.Contracts;
using GraphQL.Demo.API.GraphQL.Types;
using GraphQL.Types;

namespace GraphQL.Demo.API.GraphQL.Queries
{
    public class AppQuery : ObjectGraphType
    {
        public AppQuery(IUserRepository userRepository, IAccountRepository accountRepository)
        {
            Field<ListGraphType<UserType>>("users")
               .ResolveAsync(async context => await userRepository.GetAllAsync());
            Field<UserType>("user")
                .Arguments(new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" }
                ))
                .ResolveAsync(async context =>
                {
                    var id = context.GetArgument<Guid>("id");
                    var owner = await userRepository.GetByIdAsync(id);
                    return owner ?? throw new ExecutionError("User not found");
                });
            Field<ListGraphType<AccountType>>("accounts")
                .Arguments(new QueryArguments(
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "userId" }
                ))
                .ResolveAsync(async context =>
                {
                    var ownerId = context.GetArgument<Guid>("userId");
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
