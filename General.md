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

Update your launchsettings to automatically start Nitro (GraphQL test environment). Do this for both http and https.
```json
"launchUrl": "graphql",
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


```shell
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.Sqlite
dotnet add package Microsoft.EntityFrameworkCore.Tools
dotnet add package HotChocolate.Data.EntityFramework
```

```csharp
using Microsoft.EntityFrameworkCore;
using ShopAPI.Model;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace ShopAPI.Database;

public class OrderContext : DbContext
{
    public DbSet<Order>? Orders { get; set; }

    public DbSet<OrderLine>? Orderlines { get; set; }
    public DbSet<Customer>? Customers { get; set; }

    public OrderContext(DbContextOptions<OrderContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Customer>().HasData(
            new Customer() { Id = 1, Name = "Customer 1" },
            new Customer() { Id = 2, Name = "Customer 2" }
        );
        modelBuilder.Entity<Product>().HasData(
            new Product() { Id = 1, Name = "Product 1", EanCode = "123439900", Price = 100, Weight = 300 },
            new Product() { Id = 2, Name = "Product 2", EanCode = "37034034039", Price = 200, Weight = 700 }
        );
        modelBuilder.Entity<Order>().HasData(
            new List<Order>()
            {
                new()
                {
                    Id = 1, CustomerId = 1, OrderTime = new DateTime(2025, 5, 21, 12, 30, 30)
                },
                new()
                {
                    Id = 2, CustomerId = 2, OrderTime = new DateTime(2025, 5, 21, 9, 15, 00)
                }
            }
        );
        modelBuilder.Entity<OrderLine>().HasData(
            new OrderLine() { Id = 2, OrderId = 1, ProductId = 1, Quantity = 2, DiscountPercentage = 0 },
            new OrderLine() { Id = 1, OrderId = 1, ProductId = 2, Quantity = 5, DiscountPercentage = 5 },
            new OrderLine() { Id = 3, OrderId = 2, ProductId = 1, Quantity = 7, DiscountPercentage = 0 },
            new OrderLine() { Id = 4, OrderId = 2, ProductId = 2, Quantity = 10, DiscountPercentage = 20.0m }
        );
    }
}


```

```csharp
builder.Services.AddDbContextFactory<OrderContext>(
    options => options.UseSqlite("Data Source=Orders.db")
        .EnableSensitiveDataLogging()).AddLogging(Console.WriteLine);
       
```

```csharp
builder.Services.AddGraphQLServer()
    .AddQueryType<OrderQuery>()
    .RegisterDbContextFactory<OrderContext>();
```


```shell
Add-Migration Initial
```

```shell
Update-Database
```

```csharp
public IEnumerable<Order> GetOrdersEnumerable(OrderContext orderContext) =>
    orderContext.Orders
        .Include(order => order.OrderLines).ThenInclude(orderline => orderline.Product)
        .Include(order => order.Customer);
```

```graphql
query getOrdersFromDb {
  ordersEnumerable {
    ...orderInfo
  }
}
```

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
query allOrdersFromDb {
  ordersEnumerable {
    ...orderInfo
  }
}
```

```csharp
[UseProjection]
[UseFiltering]
public IQueryable<Order> GetOrders(OrderContext orderContext) => orderContext.Orders;
```

```csharp
builder.Services.AddGraphQLServer()
    .AddQueryType<OrderQuery>()
    .RegisterDbContextFactory<OrderContext>()
    .AddProjections()
    .AddFiltering();
```

```graphql
query allOrdersFiltering {
  orders(where: { id: {eq: 1}}) {
    ...orderInfo
  }
}
```

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

```csharp
[UsePaging(DefaultPageSize = 1)]
[UseProjection]
[UseFiltering]
[UseSorting]
public IQueryable<Order> GetOrders(OrderContext orderContext) => orderContext.Orders;
```

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
