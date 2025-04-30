# Step 9: Filtering

In [Step 5](./Step5.md), you added a parameter to a method just to be able to fetch a specific record. This functionality is supported in GraphQL out of the box by using filters. In this step, you are going to add support for this in your code.

## Update Code

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

Just like SQL, the `where` keyword is used to indicate that you want to filter the results. `And` and `or` filters and a combination of these are allowed. 
A lot of operators to check for equality and boundaries are supported. 

If you want to learn more about the rich possibilities, please check [HotChocolate Filtering Documentation](https://chillicream.com/docs/hotchocolate/v15/fetching-data/filtering).

Run the query and verify that the result looks like this.

![Query output](./images/Filtering%20output.png)

Check the Output window. You can see that the query sent to the database does use the filter.

![Query logging for filtering](./images/Filtering%20works.png)

## Extra Exercise

Add a query to select only those orders where all order lines contain more than 5 items.

[Next step](./Step10.md)



