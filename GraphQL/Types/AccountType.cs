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
            Field(x => x.UserId, type: typeof(IdGraphType)).Description("UserId property from the account object.");
            Field<UserType>("user").Resolve(context => context.Source.User).Description("User property from the account object.");
        }
    }
}
