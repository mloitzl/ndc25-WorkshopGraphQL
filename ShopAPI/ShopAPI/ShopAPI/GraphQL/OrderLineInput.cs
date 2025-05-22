namespace ShopAPI.GraphQL;

public class OrderLineInput
{
    public ProductInput Product { get; set; }
    public int Quantity { get; set; }
}