# Step 8: Prevent overfetching

```csharp
[UseProjection]
public IQueryable<Order> GetOrders(OrderContext orderContext) => orderContext.Orders;
```


```csharp
builder.Services.AddGraphQLServer()
    .AddQueryType<OrderQuery>()
    .RegisterDbContextFactory<OrderContext>()
    .AddProjections();
```

```graphql
query allOrders {
  orders {
    ...orderInfo
  }
}
```

![No overfetching](./images/No%20overfetching.png)