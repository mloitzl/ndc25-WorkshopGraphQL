namespace ShopAPI.GraphQL
{
    public class CustomerInput
    {
        public required string Name { get; set; }
        public string? EMailAddress { get; set; }
    }

    public class ProductInput
    {
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
    }

    public class OrderLineInput
    {
        public ProductInput Product { get; set; }
        public int Quantity { get; set; }
    }

    public class OrderInput
    {
        public CustomerInput Customer { get; set; } = null!;
        public List<OrderLineInput> OrderLines { get; set; } = new List<OrderLineInput>();  
    }
}
