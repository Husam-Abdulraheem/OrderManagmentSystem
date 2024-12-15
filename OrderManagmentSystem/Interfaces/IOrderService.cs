using OrderManagementSystem.Models;

namespace OrderManagementSystem.Interfaces
{
    public interface IOrderService
    {
        Task<List<OrderDTO>> GetAllOrders();
        Task<OrderDTO> AddOrder(OrderDTO order);
        Task<List<OrderDTO>> GetOrdersByRetailer(int retailerId);
        Task<List<OrderDTO>> GetOrdersBySupplier(int supplierId);
        Task<object> ChangeState(int orderId, string state);
    }
}
