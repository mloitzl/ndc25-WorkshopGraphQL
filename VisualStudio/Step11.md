# Step 11: Paging

Older version used paging with skip and take support. The new version works with streaming.


Streaming

```csharp
[UsePaging(DefaultPageSize = 1)]
[UseProjection]
[UseFiltering]
[UseSorting]
public IQueryable<Order> GetOrders(OrderContext orderContext) => orderContext.Orders;
```
For demo purposes we are going to use a page size of 1, because we don't have that much data to work with. In a real application, you would of course set this value to a more meaningfull value.


You do not need to change anything in the program.cs for this to work.

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

![Result first query](./images/Output%20first%20query.png)

Please insert the value of your own cursor into the code sample.
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

![Result next results](./images/Result%20pages%20page%202.png)

