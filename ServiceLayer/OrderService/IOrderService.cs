using Domain;
using System.Collections.Generic;

namespace ServiceLayer.OrderService
{
    public interface IOrderService
    {
        void MakeOrder(List<Product> products, User user);
        void DeleteOrder(int orderId);
        Order GetOrderById(int id);
        List<Order> GetAllOrders();
    }
}
