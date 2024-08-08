using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.Interfaces;
using OrderManagementSystem.Models;

namespace OrderManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [Route("All")]
        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _orderService.GetAllOrders();
            if (orders == null || !orders.Any())
            {
                return NotFound(new { Message = "No orders found." });
            }
            return Ok(orders);
        }

        [Route("Add")]
        [HttpPost]
        public async Task<IActionResult> AddOrder([FromBody] OrderDTO order)
        {
            //if (User.FindFirstValue("Role") != "Business")
            //{
            //    return Unauthorized();
            //}

            try
            {
                var newOrder = await _orderService.AddOrder(order);
                return CreatedAtAction(nameof(AddOrder), new { id = newOrder.OrderId }, newOrder);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        [Route("retailer/{retailerId}")]
        [HttpGet]
        public async Task<IActionResult> GetOrdersByRetailer(int retailerId)
        {
            var orders = await _orderService.GetOrdersByRetailer(retailerId);
            if (orders == null)
            {
                return NotFound("No orders found.");
            }

            return Ok(orders);
        }

        [Route("supplier/{supplierId}")]
        [HttpGet]
        public async Task<IActionResult> GetOrdersBySupplier(int supplierId)
        {
            var orders = await _orderService.GetOrdersBySupplier(supplierId);
            if (orders == null)
            {
                return NotFound("No orders found.");
            }

            return Ok(orders);
        }

    }
}