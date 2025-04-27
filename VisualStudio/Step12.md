# Step 12 Adding paging

Many client applications do support paging in the user interface. So, it is good to provide support for paging in the service as well. 
Paging in the current version of HotChocolate is based around streaming. In modern user interfaces this pattern is use more than the navigate by page number approach. In older version the default support for paging was using the skip and take pattern. 

## Change the code

Adding support for paging only requires adding the attribute UsePaging to the method.

```csharp
[UsePaging(DefaultPageSize = 1)]
[UseProjection]
[UseFiltering]
[UseSorting]
public IQueryable<Order> GetOrders(OrderContext orderContext) => orderContext.Orders;
```

