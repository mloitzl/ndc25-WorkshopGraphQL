using Microsoft.EntityFrameworkCore;
using ShopAPI.Database;
using ShopAPI.GraphQL;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContextFactory<OrderContext>(
        optionsBuilder =>
            optionsBuilder
                .UseSqlite("Data Source=Orders.db")
                .EnableSensitiveDataLogging())
    .AddLogging();

builder.Services.AddGraphQLServer()
    .AddQueryType<OrderQuery>()
    .RegisterDbContextFactory<OrderContext>()
    .AddProjections()
    .AddFiltering()
    .AddSorting()
    .ModifyPagingOptions(
        options =>
        {
            options.DefaultPageSize = 1;
            options.MaxPageSize = 10;
        }
    ).AddMutationType<OrderMutation>();

WebApplication app = builder.Build();
app.MapGraphQL();

app.MapGet("/", () => "Hello World!");
app.Run();