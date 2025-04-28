# Step 8: Prevent overfetching

Fortunately, HotChocolate has a good option to prevent overfetching. All you need for this is to apply an attribute. But because the queries you write during this workshop are meant to be usable as a reference afterward, you are creating a new method for this. 

Copy the following code to your query. The UseProjection attribute is all that you need to instruct HotChocolate to limit the field that are requested from the database. This attribute does require you to return a IQueryable. And because of this return type, you can simply return the Orders from the context.

```csharp
[UseProjection]
public IQueryable<Order> GetOrders(OrderContext orderContext) => orderContext.Orders;
```
Adding projection support does require an additional step when registering the Query type in program.cs. 
You need to add the AddProjection after the Query registration. Update you code to match the code below.

```csharp
builder.Services.AddGraphQLServer()
    .AddQueryType<OrderQuery>()
    .RegisterDbContextFactory<OrderContext>()
    .AddProjections();
```

Run the application again.

## Verify query

Add the following query to your Request pane and run this query

```graphql
query allOrders {
  orders {
    ...orderInfo
  }
}
```

Check in the Output window that no overfetching takes place any more.

![No overfetching](./images/No%20Overfetching%20Rider.png)

So now we not only do not transmit unnecessary information over the wire, but we also do not fetch this information from the database.