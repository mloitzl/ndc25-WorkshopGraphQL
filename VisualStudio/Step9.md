# Step 9: Filtering

In [step 5](./Step5.md) you added a parameter to a method just to be able to fetch a specific record. This functionality is supported in GraphQL out of the box by using filters. In this step you are going to add support for this in your code.

## Update code

HotChocolate makes it easy to work with filtering in GraphQL.
Just like you did with projections, it only requires adding an attribute to the method.

Update your code to match the code below.
```csharp
[UseProjection]
[UseFiltering]
public IQueryable<Order> GetOrders(OrderContext orderContext) => orderContext.Orders;
```
And you need to add filtering support when registering the service.

```csharp
builder.Services.AddGraphQLServer()
    .AddQueryType<OrderQuery>()
    .RegisterDbContextFactory<OrderContext>()
    .AddProjections()
    .AddFiltering();
```

If you now run your code, we can make use of special filter option is the GraphQL language.

Add the following query to your request pane.

```graphql
query allOrdersFiltering {
  orders(where: { id: {eq: 1}}) {
    ...orderInfo
  }
}
```



![Query output](./images/Filtering%20output.png)

https://chillicream.com/docs/hotchocolate/v15/fetching-data/filtering

![Query logging for filtering](./images/Filtering%20works.png)



