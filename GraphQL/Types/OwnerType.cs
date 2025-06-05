using GraphQL.DataLoader;
using GraphQL.Demo.API.Contracts;
using GraphQL.Demo.API.Entities;
using GraphQL.Types;

namespace GraphQL.Demo.API.GraphQL.Types
{
    public class OwnerType : ObjectGraphType<Owner>
    {
        public OwnerType(IAccountRepository accountRepository, IDataLoaderContextAccessor dataLoader)
        {
            Field(x => x.Id, type: typeof(IdGraphType)).Description("Id property from the owner object.");
            Field(x => x.Name).Description("Name property from the owner object.");
            Field(x => x.Address).Description("Address property from the owner object.");
            Field<ListGraphType<AccountType>>("accounts")
                .Arguments(new QueryArguments(
                    new QueryArgument<EnumerationGraphType<TypeOfAccount>> { Name = "typeof" }
                ))
                .ResolveAsync(async context =>
                {
                    var type = context.GetArgument<TypeOfAccount>("typeof");
                    var loader = dataLoader.Context.GetOrAddCollectionBatchLoader<Guid, Account>(
                        "GetAllByOwnersAsync",
                        (ids, cancellationToken) => accountRepository.GetAllByOwnersAsync(ids, type)
                    );
                    return loader.LoadAsync(context.Source.Id);
                })
                .Description("Accounts property from the owner object.");
        }
    }

    /**
     Field<ListGraphType<AccountType>>("accounts")
                .Arguments(new QueryArguments(
                    new QueryArgument<EnumerationGraphType<TypeOfAccount>> { Name = "typeof" }
                ))
                .Resolve(context =>
                {
                    var type = context.GetArgument<TypeOfAccount>("typeof");
                    var loader = dataLoader.Context.GetOrAddCollectionBatchLoader<Guid, Account>(
                        "GetAllByOwnersAsync",
                        (ids, cancellationToken) => accountRepository.GetAllByOwnersAsync(ids, type)
                    );
                    return loader.LoadAsync(context.Source.Id);
                })
                .Description("Accounts property from the owner object.");
    **/
}
