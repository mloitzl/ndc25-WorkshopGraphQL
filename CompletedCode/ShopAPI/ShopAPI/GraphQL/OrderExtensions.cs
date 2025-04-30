using ShopAPI.Model;
using System.Runtime.CompilerServices;

namespace ShopAPI.GraphQL
{
    public static class OrderExtensions
    {
        public static Order ToOrder(this OrderInput orderInput)
        {
            var customer = new Model.Customer()
            {
                Name = orderInput.Customer.Name,
                EMailAddress = orderInput.Customer.EMailAddress
            };

            var order = new Model.Order()
            {
                Customer = customer,
                OrderStatus = Model.OrderStatus.NEW,
                OrderTime = DateTime.Now
            };

            foreach (var orderline in orderInput.OrderLines)
            {
                var product = new Model.Product() { Name = orderline.Product.Name };
                var modelOrderLine = new Model.OrderLine()
                {
                    Product = product,
                    Quantity = orderline.Quantity
                };

                order.OrderLines.Add(modelOrderLine);

            }

            return order;
        }

        public static OrderPayload ToPayload(this Order order)
        { 
            var orderPayload = new OrderPayload()
            {
                Order = order
            };

            return orderPayload;
        }

    }
}
