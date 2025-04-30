using ShopAPI.Database;

namespace ShopAPI.GraphQL
{
    public class OrderMutation
    {
        public async Task<OrderPayload> CreateOrderAsync(OrderContext orderContext,
            OrderInput orderInput)
        {
            var order = orderInput.ToOrder();
            
            await orderContext.AddAsync(order);
            await orderContext.SaveChangesAsync();

            return order.ToPayload();
        }
    }
}
