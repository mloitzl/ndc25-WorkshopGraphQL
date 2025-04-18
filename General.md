```csharp
namespace ShopAPI.Model;

public class Customer
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public string? EMailAddress { get; set; }
}
```

```csharp
namespace ShopAPI.Model;

public class Product
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public string? EanCode { get; set; }

    public decimal Price { get; set; }

    public decimal Weight { get; set; }
}
```

```csharp
namespace ShopAPI.Model;

public class OrderLine
{
    public int Id { get; set; }

    public int OrderId { get; set; }

    public Order Order { get; set; } = null!;

    public Product Product { get; set; } = null!;

    public int ProductId { get; set; }

    public int Quantity { get; set; } = 1;

    public decimal DiscountPercentage { get; set; } = 0;
}
```

```csharp
namespace ShopAPI.Model;

public class Order
{
    public int Id { get; set; }

    public ICollection<OrderLine> OrderLines { get; set; } = new List<OrderLine>(); 

    public Customer Customer { get; set; } = null!;
    public int CustomerId { get; set; }

    public DateTime OrderTime { get; set; }

    public OrderStatus OrderStatus { get; set; }
}
```

```csharp
namespace ShopAPI.Model;

public enum OrderStatus
{
    NEW,
    PAID,
    OPENTOPICK,
    SHIPPED,
    DELIVERED,
    RETURNED
}
```

Of course, you can also use the NuGet tools within Rider/VS
```shell
dotnet add package HotChocolate.AspNetCore
```

```csharp
public class OrderQuery
{
}
```

```csharp
private List<Order> GenerateTestOrders()
{
    var order1 = new Order()
    {
        Id = 1,
        Customer = new Customer()
        {
            Id = 1, Name = "Someone", EMailAddress = "noreply@nowhere.com"
        },
        OrderStatus = OrderStatus.PAID,
        OrderTime = DateTime.Now
    };

    order1.OrderLines.Add(new OrderLine()
    {
        Id = 1,
        Order = order1,
        Product =
            new Product() {Id = 1, Name = "Samsung S20", Price = 900, Weight = 100, EanCode = "4985791347598"},
        Quantity = 2,
        DiscountPercentage = 20
    });
    order1.OrderLines.Add(new OrderLine()
    {
        Id = 2,
        Order = order1,
        Product = new Product()
            {Id = 1, Name = "iPhone 13", Price = 1200, Weight = 100, EanCode = "498430958307598"},
        Quantity = 1,
        DiscountPercentage = 10
    });

    var order2 = new Order()
    {
        Id = 2,
        Customer = new Customer()
        {
            Id = 1, Name = "Iris", EMailAddress = "whoami@nowhere.com"
        },
        OrderStatus = OrderStatus.SHIPPED,
        OrderTime = DateTime.Now.AddDays(-2)
    };

    order2.OrderLines.Add(new OrderLine()
    {
        Id = 1,
        Order = order1,
        Product = new Product() {Id = 1, Name = "Clean code", Price = 20, Weight = 200, EanCode = "94359324590380"},
        Quantity = 1,
        DiscountPercentage = 0
    });
    order2.OrderLines.Add(new OrderLine()
    {
        Id = 2,
        Order = order1,
        Product = new Product()
        {
            Id = 1, Name = "The unlikely success of a copy-paste developer", Price = 30, Weight = 150,
            EanCode = "87439587975927"
        },
        Quantity = 2,
        DiscountPercentage = 5
    });

    return [order1, order2];
}
```

```csharp
public IEnumerable<Order> GetAllOrders()
{
    return GenerateTestOrders();
}
```

```csharp
builder.Services.AddGraphQLServer()
    .AddQueryType<OrderQuery>();
```

```csharp
app.MapGraphQL();
```

```graphql
query {
  allOrders {
    customer {
      name
    }
    orderLines{
      product {
        name
      }
      quantity
    }
  }
}
```

```csharp
public Order GetOrderById(int id)
{
    return GenerateTestOrders().Single(order => order.Id == id);
}
```

```graphql
query getOrdersById {
  orderById(id: 1){
    id
    customer {
      name
    }
    orderLines{
      product {
        name
      }
      quantity
    }
  }
}
```

```graphql
query getOrdersById($id: Int!) {
  orderById(id: $id){
    id
    customer {
      name
    }
    orderLines{
      product {
        name
      }
      quantity
    }
  }
}
```

```graphql
fragment orderInfo on Order {
  id
  customer {
    name
  }
  orderStatus
  orderTime
  orderLines {
    product {
      name
    }
    quantity
  }
}
```

I did give the first query a name as well
```graphql
query getAllOrders {
  allOrders {
    ...orderInfo
  }
}

query getOrdersById($id: Int!) {
  orderById(id: $id){
    ...orderInfo
  }
}

```

