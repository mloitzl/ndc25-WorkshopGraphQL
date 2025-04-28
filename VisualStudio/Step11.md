# Step 11 Adding paging

Many client applications do support paging in the user interface. So, it is good to provide support for paging in the service as well. 
Paging in the current version of HotChocolate is based around streaming. In modern user interfaces this pattern is use more than the navigate by page number approach. In older version the default support for paging was using the skip and take pattern. 

## Change the code

Adding support for paging only requires adding the attribute UsePaging to the method.

Update your code to match this code. Please pay attention to the order in which the attributes are applied.

```csharp
[UsePaging(DefaultPageSize = 1)]
[UseProjection]
[UseFiltering]
[UseSorting]
public IQueryable<Order> GetOrders(OrderContext orderContext) => orderContext.Orders;
```

A default pagesize of 1 is of course not very usefull, but because we only use a small test set during this workshop, this value is chosen on purpose to demonstrate the use of cursors.

Adding support for paging does not require a change in the program.cs. You can however specify the default page size. This would like the following code. You do not need to change your code. The code is just shown as an example.

```csharp
builder.Services.AddGraphQLServer()
    .AddQueryType<OrderQuery>()
    .RegisterDbContextFactory<OrderContext>()
    .AddProjections()
    .AddFiltering()
    .AddSorting()
    .ModifyPagingOptions(options =>
    {
        options.DefaultPageSize = 1;
        options.MaxPageSize = 10;
    });
```

If you now run your run, you can use paging in your queries. The page information is returned with the fields pageInfo and edges. The cursor that is returned is needed to request the next or previous batch of objects.

Copy this query to your own request pane and run it.

```graphql
query allOrdersPaged {
  orders {
    pageInfo {
      hasNextPage
      hasPreviousPage
    }
    edges {
      cursor
    }
    nodes {
      ...orderInfo
    }
  }
}
```

Your output should look like this.
![Output paging page 1](./images/Result%20pages.png)

In the result the information about the availability of a next and previous page is returned together with a cursor. This cursor can be used to fetch the next batch.

Due to the change in the return type, the queries for the other features don't work any more without changing the response. 

Copy the following query to your request pane, but do replace the value for the cursor with the returned value from the previous query. 

```graphql
query allOrdersSorted {
  orders(after: "MA==" ) {
    pageInfo {
      hasNextPage
      hasPreviousPage
    }
    edges {
      cursor
    }
    nodes {
      ...orderInfo
    }
  }
}
```

If you now run this query, you will get an output simular to the following picture.
![Output page 2](./images/Result%20pages%20page%202.png)





