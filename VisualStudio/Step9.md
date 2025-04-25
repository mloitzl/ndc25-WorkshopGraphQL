#Step 9: Filtering

In, but GraphQL has excellent built-in support for this. Let's update the code to use this functionality.

```csharp
[UseProjection]
[UseFiltering]
public IQueryable<Order> GetOrders(OrderContext orderContext) => orderContext.Orders;
```

```csharp
builder.Services.AddGraphQLServer()
    .AddQueryType<OrderQuery>()
    .RegisterDbContextFactory<OrderContext>()
    .AddProjections()
    .AddFiltering();
```

```graphql
query allOrdersFiltering {
  orders(where: { id: {eq: 1}}) {
    ...orderInfo
  }
}
```

![Query output](./images/Filtering%20output.png)



![Query logging for filtering](./images/Filtering%20works.png)



