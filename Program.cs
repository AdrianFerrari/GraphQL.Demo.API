using GraphQL;
using GraphQL.Demo.API.Contracts;
using GraphQL.Demo.API.Entities.Context;
using GraphQL.Demo.API.GraphQL.GraphQLSchema;
using GraphQL.Demo.API.Repository;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    options.AllowInputFormatterExceptionMessages = false;
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
builder.Services.AddDbContextFactory<ApplicationContext>(opt =>
                opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<AppSchema>();


builder.Services.AddGraphQL(options =>
{
    options.AddSystemTextJson();
    options.AddGraphTypes();
    options.AddDataLoader();
});

var app = builder.Build();
app.UseRouting();
app.UseAuthorization();
app.UseGraphQL<AppSchema>();

app.Run();
