using System.ComponentModel.DataAnnotations;

namespace GraphQL.Demo.API.GraphQL.Requests
{
    public class UserInput
    {
        public required string Name { get; set; }
        [EmailAddress]
        public required string Email { get; set; }
    }
}
