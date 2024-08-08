using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.Data;
using OrderManagementSystem.Data.Models;
using OrderManagementSystem.Interfaces;
using OrderManagementSystem.Models;

namespace OrderManagementSystem.Services
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _environment;

        public OrderService(ApplicationDbContext db, IWebHostEnvironment environment)
        {
            _db = db;
            _environment = environment;
        }

        public async Task<List<Order>> GetAllOrders()
        {
            try
            {
                return await _db.Orders
                    .Include(o => o.OrderItems)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error while retrieving all orders.", ex);
            }
        }

        public async Task<OrderDTO> AddOrder(OrderDTO dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto), "Order cannot be null.");

            if (dto.Items == null || !dto.Items.Any())
            {
                throw new ArgumentException("Order must contain at least one product.", nameof(dto));
            }

            Order order = new()
            {
                RetailerId = dto.RetailerId,
                OrderDate = dto.OrderDate,
                OrderItems = new List<OrderItem>()
            };

            foreach (var item in dto.Items)
            {
                OrderItem orderItem = new()
                {
                    ProductId = item.ProductId,
                    SupplierId = item.SupplierId,
                    Quantity = item.Quantity,
                };
                order.OrderItems.Add(orderItem);
            }

            _db.Orders.Add(order);
            await _db.SaveChangesAsync();

            dto.OrderId = order.Id;

            return dto;
        }

        public async Task<List<OrderDTO>> GetOrdersByRetailer(int retailerId)
        {
            var orders = await _db.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product) // Include Product if needed
                .Where(o => o.RetailerId == retailerId)
                .ToListAsync();

            var orderDtos = new List<OrderDTO>();

            foreach (var order in orders)
            {
                var groupedItems = order.OrderItems
                    .GroupBy(item => item.SupplierId)
                    .Select(g => new
                    {
                        SupplierId = g.Key,
                        Items = g.Select(item => new OrderItemDTO
                        {
                            ProductId = item.ProductId,
                            SupplierId = item.SupplierId,
                            Quantity = item.Quantity
                        }).ToList()
                    })
                    .ToList();

                foreach (var group in groupedItems)
                {
                    var orderDto = new OrderDTO
                    {
                        OrderId = order.Id,
                        RetailerId = order.RetailerId,
                        OrderDate = order.OrderDate,
                        Items = group.Items
                    };

                    orderDtos.Add(orderDto);
                }
            }

            return orderDtos;
        }



        public async Task<List<OrderDTO>> GetOrdersBySupplier(int supplierId)
        {
            var orderItems = await _db.OrderItems
                .Include(oi => oi.Order)
                .Where(oi => oi.SupplierId == supplierId)
                .ToListAsync();

            var orderDtos = orderItems.GroupBy(oi => oi.OrderId)
                .Select(group => new OrderDTO
                {
                    OrderId = group.First().OrderId,
                    RetailerId = group.First().Order.RetailerId,
                    OrderDate = group.First().Order.OrderDate,
                    Items = group.Select(item => new OrderItemDTO
                    {
                        ProductId = item.ProductId,
                        SupplierId = item.SupplierId,
                        Quantity = item.Quantity
                    }).ToList()
                }).ToList();

            return orderDtos;
        }
    }
}