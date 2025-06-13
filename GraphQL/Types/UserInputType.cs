using GraphQL.Types;

namespace GraphQL.Demo.API.GraphQL.Types
{
    public class UserInputType : InputObjectGraphType
    {
        public UserInputType()
        {
            Name = "UserInput";
            Field<NonNullGraphType<StringGraphType>>("name").Description("Name from the user.");
            Field<NonNullGraphType<StringGraphType>>("email").Description("Email the user.");
        }
    }
}
