using OrderManagementSystem.Data.Models;
using OrderManagementSystem.Models;

namespace OrderManagementSystem.Interfaces
{
    public interface IOrderService
    {
        Task<List<Order>> GetAllOrders();
        Task<OrderDTO> AddOrder(OrderDTO order);
        Task<List<OrderDTO>> GetOrdersByRetailer(int retailerId);
        Task<List<OrderDTO>> GetOrdersBySupplier(int supplierId);
    }
}
