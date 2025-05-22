namespace ShopAPI.GraphQL;

public class OrderInput
{
    public CustomerInput Customer { get; set; } = null!;
    public List<OrderLineInput> OrderLines { get; set; } = new List<OrderLineInput>();  
}