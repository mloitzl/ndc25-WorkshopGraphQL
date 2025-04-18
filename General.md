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