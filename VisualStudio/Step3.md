# Step 3 Create basic GraphQL Server
In this step you are going to create a very basic GraphQL server using HotChocolate for the classes you created in [Step 2](Step2.md).

## Register NuGet package

Register the following package in your project. If you want, you can also use the NuGet Package Manager of course to complete this step.

```shell
dotnet add package HotChocolate.AspNetCore
```

## Create folder GraphQL
To keep the GraphQL code seperate from the rest of the code, you will have to create a folder named GraphQL in your project.

## Query class
Fetching data can be done in GraphQL through a so called Query-class. This is a normal POCO class that does not require you to implement an interface or apply an attribute.

In the folder you just created, create a new class named OrderQuery and make sure the code looks like the code shown below.

```csharp
public class OrderQuery
{
}
```

### Test data
We do want to provide a method to allow the user of the API to fetch all orders. Since we don’t have a database yet, we do need to get a way to get a list of orders. We will implement a helper method within the OrderQuery class to generate a list of Orders. Create the following helper method in the Query class.

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
### Query method
Now we can use this helper method in a query method. Create this method in the Query class.

```csharp
public IEnumerable<Order> GetAllOrders()
{
    return GenerateTestOrders();
}
```

## Register GraphQL services
For GraphQL to work, you need to register the GraphQL services and specify that you want to use the OrderQuery class as the Query for this server. Add the following code before the line with builder.Build() to achieve both these things.

```csharp
builder.Services.AddGraphQLServer()
    .AddQueryType<OrderQuery>();
```

## Add to pipeline
The server will have to be able to intercept GraphQL request. Therefore you need to add GraphQL to the pipeline. Add the following code before the line containing the app.Run().

```csharp
app.MapGraphQL();
```

This code will map the endpoint /graphql to your GraphQL code.

## Set the GraphQL endpoint as default for the project

When building a GraphQL server, you will have to build and run the test environment many timees. Therfore it is convenient to register the GraphQL endpoint as default page to open when debugging the application. Add the following code to the lauchsettings.json file for the http and https endpoint.

```json
"launchUrl": "graphql",
```


[Next step](./Step4.md)


