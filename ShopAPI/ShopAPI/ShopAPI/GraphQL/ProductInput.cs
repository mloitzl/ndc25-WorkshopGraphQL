namespace ShopAPI.GraphQL;

public class ProductInput
{
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
}