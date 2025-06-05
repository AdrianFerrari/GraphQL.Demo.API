using GraphQL.Demo.API.Entities;
using GraphQL.Types;

namespace GraphQL.Demo.API.GraphQL.Types
{
    public class AccountType : ObjectGraphType<Account>
    {
        public AccountType()
        {
            Field(x => x.Id, type: typeof(IdGraphType)).Description("Id property from the account object.");
            Field<NonNullGraphType<EnumerationGraphType<TypeOfAccount>>>("Type").Description("Type property from the account object.");
            Field(x => x.Description).Description("Description property from the account object.");
            Field(x => x.OwnerId, type: typeof(IdGraphType)).Description("OwnerId property from the account object.");
            Field<OwnerType>("owner").Resolve(context => context.Source.Owner).Description("Owner property from the account object.");
        }
    }
}
