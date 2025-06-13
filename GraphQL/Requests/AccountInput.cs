using GraphQL.Demo.API.Entities;

namespace GraphQL.Demo.API.GraphQL.Requests
{
    public class AccountInput
    {
        public TypeOfAccount Type { get; set; }
        public string Description { get; set; } = "";
    }
}
