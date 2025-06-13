using GraphQL.DataLoader;
using GraphQL.Demo.API.Contracts;
using GraphQL.Demo.API.Entities;
using GraphQL.Types;

namespace GraphQL.Demo.API.GraphQL.Types
{
    public class UserType : ObjectGraphType<User>
    {
        public UserType(IAccountRepository accountRepository, IDataLoaderContextAccessor dataLoader)
        {
            Field(x => x.Id, type: typeof(IdGraphType)).Description("Id from the user.");
            Field(x => x.Name).Description("Name of the user.");
            Field(x => x.Email).Description("Email of the user.");
            Field<ListGraphType<AccountType>>("accounts")
                .Arguments(new QueryArguments(
                    new QueryArgument<EnumerationGraphType<TypeOfAccount>> { Name = "typeof" }
                ))
                .Resolve(context =>
                {
                    var type = context.GetArgument<TypeOfAccount?>("typeof");
                    var loader = dataLoader.Context.GetOrAddCollectionBatchLoader<Guid, Account>(
                        "GetAllByOwnersAsync",
                        (ids, cancellationToken) => accountRepository.GetAllByOwnersAsync(ids, type)
                    );
                    return loader.LoadAsync(context.Source.Id);
                })
                .Description("Accounts from the user.");
        }
    }
}
