using GraphQL.Demo.API.Entities;
using GraphQL.Types;

namespace GraphQL.Demo.API.GraphQL.Types
{
    public class AccountInputType : InputObjectGraphType
    {
        public AccountInputType()
        {
            Name = "AccountInput";
            Field<NonNullGraphType<EnumerationGraphType<TypeOfAccount>>>("type").Description("Type of account.");
            Field<NonNullGraphType<StringGraphType>>("description").Description("Description of the account.");
        }
    }
}
