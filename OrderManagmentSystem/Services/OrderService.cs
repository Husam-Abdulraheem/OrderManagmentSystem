using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.Interfaces;
using OrderManagementSystem.Models.OrderFolder;
using OrderManagmentSystem.Models;
using OrderManagmentSystem.Models.OrderFolder;

namespace OrderManagementSystem.Services
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IWebHostEnvironment _environment;

        public OrderService(ApplicationDbContext dbContext, IWebHostEnvironment environment)
        {
            _dbContext = dbContext;
            _environment = environment;
        }

        public async Task<List<Order>> GetAllOrders()
        {
            try
            {
                return await _dbContext.Orders
                    .Include(o => o.ProductInOrder)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error while retrieving all orders.", ex);
            }
        }

        public async Task<Order> ReceiveOrder(Order order)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order), "Order cannot be null.");

            if (order.ProductInOrder == null || !order.ProductInOrder.Any())
                throw new ArgumentException("Order must contain at least one product.", nameof(order));

            // Add the order to the database
            _dbContext.Orders.Add(order);
            await _dbContext.SaveChangesAsync();

            // Split the order between suppliers
            await SplitOrderAsync(order);

            return order;
        }

        public async Task SplitOrderAsync(Order order)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order), "Order cannot be null.");

            if (order.ProductInOrder == null || !order.ProductInOrder.Any())
                throw new ArgumentException("Order must contain at least one product.", nameof(order));

            var supplierOrders = new Dictionary<int, SupplierOrder>();

            foreach (var item in order.ProductInOrder)
            {
                if (!supplierOrders.ContainsKey(item.SupplierId))
                {
                    supplierOrders[item.SupplierId] = new SupplierOrder
                    {
                        SupplierId = item.SupplierId,
                        OrderDate = order.OrderDate,
                        RetailerId = order.RetailerId,
                        SupplierOrderItems = new List<SupplierOrderItem>()
                    };
                }

                var supplierOrderItem = new SupplierOrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    TotalPrice = (float)(item.Quantity * GetProductPrice(item.ProductId))
                };

                supplierOrders[item.SupplierId].SupplierOrderItems.Add(supplierOrderItem);
                supplierOrders[item.SupplierId].Total += supplierOrderItem.TotalPrice;
            }

            _dbContext.SupplierOrders.AddRange(supplierOrders.Values);
            await _dbContext.SaveChangesAsync();
        }

        private double GetProductPrice(int productId)
        {
            // Replace this with the actual logic to get product price from the database or another service
            return 10.0;
        }
    }
}