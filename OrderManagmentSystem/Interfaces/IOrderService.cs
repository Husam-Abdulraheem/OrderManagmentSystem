using OrderManagmentSystem.Models.OrderFolder;

namespace OrderManagementSystem.Interfaces
{
    public interface IOrderService
    {
        Task<List<Order>> GetAllOrders();
        Task<Order> ReceiveOrder(Order order);
    }
}
