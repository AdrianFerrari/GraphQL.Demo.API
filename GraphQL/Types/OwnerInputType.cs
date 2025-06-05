using GraphQL.Types;

namespace GraphQL.Demo.API.GraphQL.Types
{
    public class OwnerInputType : InputObjectGraphType
    {
        public OwnerInputType()
        {
            Name = "OwnerInput";
            Field<NonNullGraphType<StringGraphType>>("name").Description("Name property from the owner object.");
            Field<NonNullGraphType<StringGraphType>>("address").Description("Address property from the owner object.");
        }
    }
}
