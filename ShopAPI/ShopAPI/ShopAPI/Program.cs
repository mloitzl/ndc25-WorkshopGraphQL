using ShopAPI.GraphQL;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.AddGraphQLServer()
    .AddQueryType<OrderQuery>();

WebApplication app = builder.Build();
app.MapGraphQL();

app.MapGet("/", () => "Hello World!");
app.Run();