# Step 10: Adding support for sorting

Sorting information is quite common within software applications. You can of course do this on the client, or you can let the server handle it. Which one you choose, really depends on the situation. GraphQL has good support for server side sorting and so has HotChocolate.

## Change the code

Just like you did with projections and filtering, adding sorting support to a query only requires adding an attribute to the method. For sorting you need to use the attribute UseSorting.

Change your code to match the code below.
```csharp
[UseProjection]
[UseFiltering]
[UseSorting]
public IQueryable<Order> GetOrders(OrderContext orderContext) => orderContext.Orders;
```

And you need to add sorting support when registering the service.

```csharp
builder.Services.AddGraphQLServer()
    .AddQueryType<OrderQuery>()
    .RegisterDbContextFactory<OrderContext>()
    .AddProjections()
    .AddFiltering()
    .AddSorting();
```

If you now run your code, we can make use of special sorting option is the GraphQL language.
Add the following query to your request pane.

```graphql
query allOrdersSorted {
  orders(order: { customer: { name: DESC }}) {
    ...orderInfo
  }
}
```

Verify that your results look this this.

![Results sorted](./images/Result%20sorted.png)


In the Output window you can see that the sorting is actually being done in the database. 

![Sort query](./images/Sort%20query.png)

[Next step](./Step11.md)