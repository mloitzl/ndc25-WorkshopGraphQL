namespace ShopAPI.GraphQL;

public class CustomerInput
{
    public required string Name { get; set; }
    public string? EMailAddress { get; set; }
}