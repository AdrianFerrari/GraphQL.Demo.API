using GraphQL.Demo.API.GraphQL.Mutations;
using GraphQL.Demo.API.GraphQL.Queries;
using GraphQL.Types;
using GraphQL.Utilities;

namespace GraphQL.Demo.API.GraphQL.GraphQLSchema
{
    public class AppSchema : Schema
    {
        public AppSchema(IServiceProvider provider)
            : base(provider)
        {
            Query = provider.GetRequiredService<AppQuery>();
            Mutation = provider.GetRequiredService<AppMutation>();
        }
    }
}
