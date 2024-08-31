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

        public async Task<List<OrderDTO>> GetAllOrders()
        {
            try
            {
                var orders = await _db.Orders
                    .Include(o => o.OrderItems).ThenInclude(p => p.Product)
                    .ToListAsync();
                // Map the Order entities to OrderDTOs
                var orderDTOs = orders.Select(o =>
                {
                    var Items = o.OrderItems.Select(oi => new OrderItemDTO
                    {
                        ProductId = oi.ProductId,
                        Quantity = oi.Quantity,
                        SupplierId = oi.SupplierId,
                        Total = SumPrice(oi.Product.Price, oi.Quantity)
                    }).ToList();

                    var totalDeal = Items.Sum(t => t.Total);

                    return new OrderDTO
                    {
                        OrderId = o.Id,
                        OrderDate = o.OrderDate,
                        RetailerId = o.RetailerId,
                        TotalDeal = totalDeal,
                        Items = Items
                    };
                }).ToList();
                return orderDTOs;
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
            .ThenInclude(oi => oi.Product) // Include Product in OrderItems
        .Where(o => o.RetailerId == retailerId)
        .ToListAsync();

            var orderDtos = orders.Select(order =>
            {
                var Items = order.OrderItems.Select(item => new OrderItemDTO
                {
                    ProductId = item.ProductId,
                    SupplierId = item.SupplierId,
                    Quantity = item.Quantity,
                    Total = SumPrice(item.Product.Price, item.Quantity),
                    Product = new ProductDTO
                    {
                        ProductId = item.Product.Id,
                        Title = item.Product.Title,
                        Description = item.Product.Description,
                        Price = item.Product.Price,
                        CategoryId = item.Product.CategoryId,
                        SupplierId = item.Product.SupplierId,
                        viewImage = item.Product.ImageUrl
                    }
                }).ToList();

                var totalDeal = Items.Sum(t => t.Total);
                return new OrderDTO
                {
                    OrderId = order.Id,
                    RetailerId = order.RetailerId,
                    OrderDate = order.OrderDate,
                    TotalDeal = totalDeal,
                    Items = Items,
                };
            }).ToList();
            return orderDtos;
        }



        public async Task<List<OrderDTO>> GetOrdersBySupplier(int supplierId)
        {
            var orderItems = await _db.OrderItems
                .Include(oi => oi.Order).ThenInclude(r => r.Retailer).ThenInclude(u => u.User).ThenInclude(a => a.Addresses).Include(oi => oi.Product)
                .Where(oi => oi.SupplierId == supplierId)
                .ToListAsync();

            var orderDtos = orderItems.GroupBy(oi => oi.OrderId)
                .Select(group =>
                {
                    var Items = group.Select(item => new OrderItemDTO
                    {
                        ProductId = item.ProductId,
                        SupplierId = item.SupplierId,
                        Quantity = item.Quantity,
                        Total = SumPrice(item.Product.Price, item.Quantity),
                        Product = new ProductDTO
                        {
                            ProductId = item.Product.Id,
                            Title = item.Product.Title,
                            Description = item.Product.Description,
                            CategoryId = item.Product.CategoryId,
                            Price = item.Product.Price,
                            viewImage = item.Product.ImageUrl
                        }
                    }).ToList();

                    var totalDeal = Items.Sum(t => t.Total);
                    return new OrderDTO
                    {
                        OrderId = group.First().OrderId,
                        RetailerId = group.First().Order.RetailerId,
                        OrderDate = group.First().Order.OrderDate,
                        TotalDeal = totalDeal,
                        Items = Items,
                        Retailer = new RetailerDTO
                        {
                            RetailerId = group.First().Order.Retailer.Id,
                            RetailerFirstName = group.First().Order.Retailer.User.FirstName,
                            RetailerLastName = group.First().Order.Retailer.User.LastName,
                            BusinessName = group.First().Order.Retailer.User.BusinessName,
                            RetailerPhoneNumber = group.First().Order.Retailer.User.PhoneNumber,
                            Logo = group.First().Order.Retailer.User.LogoUrl,
                            RetailerAddress = group.First().Order.Retailer.User.Addresses,
                        },
                    };
                }).ToList();

            return orderDtos;
        }

        private float SumPrice(float price, int quantity)
        {
            return price * quantity;
        }
    }
}