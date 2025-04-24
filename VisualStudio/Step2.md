# Step 2 Create Model classes
In this step you are going to create the domain classes needed for this workshop.

## Create model directory

Just like any other solution, we do need some domain classes to work with. Just to keep our project clean, we are going to store the domain classes in their own directory in our project. Create a Model directory in your project.

![Model directory](./images/Model%20folder.png)


## Create model classes

During this workshop you are going to build an API for a webshop, so you need probably well known classes for orders, orderlines, product and customer. Let's build these classes.

Create the following classes in your Model folder.

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

