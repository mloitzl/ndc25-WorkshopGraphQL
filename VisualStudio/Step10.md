# Step 10: Adding support for sorting

```csharp
[UseProjection]
[UseFiltering]
[UseSorting]
public IQueryable<Order> GetOrders(OrderContext orderContext) => orderContext.Orders;
```

```csharp
builder.Services.AddGraphQLServer()
    .AddQueryType<OrderQuery>()
    .RegisterDbContextFactory<OrderContext>()
    .AddProjections()
    .AddFiltering()
    .AddSorting();
```


```graphql
query allOrdersSorted {
  orders(order: { customer: { name: DESC }}) {
    ...orderInfo
  }
}
```

![Results sorted](./images/Result%20sorted.png)

![Sort query](./images/Sort%20query.png)
