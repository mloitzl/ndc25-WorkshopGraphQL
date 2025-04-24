# Step 7: Add EF Core support

![NuGet Packages](./images/EF%20Core%20packages.png)

Packages:
- dshjfal
- kjdsfalkj


```shell
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.Sqlite
dotnet add package Microsoft.EntityFrameworkCore.Tools
dotnet add package HotChocolate.Data.EntityFramework
```

## Create context

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

## Register context 

```csharp
builder.Services.AddDbContextFactory<OrderContext>(
    options => options.UseSqlite("Data Source=Orders.db")
        .EnableSensitiveDataLogging()).AddLogging(Console.WriteLine);
       
```

## Add to HotChocolate

```csharp
builder.Services.AddGraphQLServer()
    .AddQueryType<OrderQuery>()
    .RegisterDbContextFactory<OrderContext>();
```

## Migrations

Perform the following commands in the NuGet Package Manager Console
![Package Manager Console](./images/Package%20managar%20console.png)


```shell
Add-Migration Initial
```

```shell
Update-Database
```

![Database created](./images/Database%20created.png)

## Add query method using EF Context

```csharp
public IEnumerable<Order> GetOrdersEnumerable(OrderContext orderContext) =>
    orderContext.Orders
        .Include(order => order.OrderLines).ThenInclude(orderline => orderline.Product)
        .Include(order => order.Customer);
```

## Use new method in Nitro

```graphql
query getOrdersFromDb {
  ordersEnumerable {
    ...orderInfo
  }
}
```

![Overfetching](./images/Overfetching.png)

