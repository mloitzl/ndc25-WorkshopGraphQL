# Step 7: Add EF Core Support

Most services do need some kind of data storage to work. In this workshop, you will be using Entity Framework Core in combination with SQLite for this. SQLite works cross-platform and is therefore better suited to support Windows, Mac, and Linux users. In this workshop, we don’t enforce a certain operating system.

## Install NuGet packages

Install the following NuGet packages by using the command line or the Package Manager Console.

```shell
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.Sqlite
dotnet add package Microsoft.EntityFrameworkCore.Tools
dotnet add package HotChocolate.Data.EntityFramework
```

After installing the list of packages should look like this.

![Installed packages](./images/EF%20Core%20installed%20packages.png)

## Create context

Before we can connect GraphQL to Entity Framework Core you need to write a DataContext for this. Put this class is its own directory named Database. Since we want to retrieve data from the database, you will add seeding to the context by specifying this data in the OnModelCreating method.

Create a new folder named Database in your project. Create a new class named OrderContext and copy the contents below into this new class.

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
This context exposes Orders, OrderLines and Customers as sets to work with. In the workshop, we will only directly use the Orders set.

The data seeding uses the ids of the objects to link objects together.


## Register context 
With this DbContext in place, we can add support for EntityFramework to our program.cs class.

Just above the code that adds the GraphQLService, add the following code:

```csharp
builder.Services.AddDbContextFactory<OrderContext>(
    options => options.UseSqlite("Data Source=Orders.db")
        .EnableSensitiveDataLogging()).AddLogging(Console.WriteLine);
       
```

This will add the DbContext to the Services collection. We do need to add a context factory due to the inner workings of GraphQL. This framework can run parallel queries and if you just register the DbContext in the normal way, this will not work. To be able to see the entire logging of EF Core, EnableSensitiveDataLogging is enabled. Please do not add sensitive data logging in your own applications, but we now need it now to test some query features later on.

## Add to HotChocolate

GraphQL should also be made aware of the Entity Framework link.
So, add the following code for RegisterDbContext after our Query registration.

```csharp
builder.Services.AddGraphQLServer()
    .AddQueryType<OrderQuery>()
    .RegisterDbContextFactory<OrderContext>();
```

## Migrations

To start working with our database, we need to generate an initial migration. We will use the package management console for this. Open this console.

![Package Manager Console](./images/Package%20managar%20console.png)

Execute the following command in this console.

```shell
Add-Migration Initial
```

This migration must be pushed to a database, so we are going to do this by running the Update Database command.

```shell
Update-Database
```
In the solution the newly created database is now visible.

![Database created](./images/Database%20created.png)

Verify that you do see the file Orders.db.

## Add query method using EF Context

You can now update your Query class to use the DbContext. We are going to introduce a new method for this.

Copy the following code to your Query class.

```csharp
public IEnumerable<Order> GetOrdersEnumerable(OrderContext orderContext) =>
    orderContext.Orders
        .Include(order => order.OrderLines).ThenInclude(orderline => orderline.Product)
        .Include(order => order.Customer);
```

With this code you can fetch all the information about your orders using your OrderContext. To be able to fetch all the dependent objects the Include and ThenInclude for OrderLines, Product and Customer is used.

Compile and run your application.

## Use new method in Nitro

We have to add a new query to our Request pane to call our new method. Copy the following query to this pane.

```graphql
query getOrdersFromDb {
  ordersEnumerable {
    ...orderInfo
  }
}
```
Run this query and verify that you see results matching the picture below

![Result query agains db](./images/Db%20connection%20result.png)

Now check the logging in the Output window of Visual Studio. You should see something like this.

![Overfetching](./images/Overfetching.png)

All the fields from the the database are still retrieved from the database, although they are not specified in the query. There is still overfetching taking place. You are going to fix this in the [next step](./Step8.md).


