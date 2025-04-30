using Microsoft.EntityFrameworkCore;
using ShopAPI.Database;
using ShopAPI.GraphQL;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContextFactory<OrderContext>(
    options => options.UseSqlite("Data Source=Orders.db")
        .EnableSensitiveDataLogging()).AddLogging(Console.WriteLine);

builder.Services.AddGraphQLServer()
    .AddQueryType<OrderQuery>()
    .RegisterDbContextFactory<OrderContext>()
    .AddProjections()
    .AddFiltering()
    .AddSorting()
    .AddMutationType<OrderMutation>();
    

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGraphQL();

app.Run();
