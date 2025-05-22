using ShopAPI.Model;

namespace ShopAPI.GraphQL;

public class OrderPayload
{
    public Order Order { get; set; } = null!;

    public string? Error { get; set; }
}