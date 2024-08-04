using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.Interfaces;
using OrderManagmentSystem.Models.OrderFolder;
using System.Security.Claims;

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

        [Route("receive")]
        [HttpPost]
        public async Task<IActionResult> ReceiveOrder([FromBody] Order order)
        {
            if (User.FindFirstValue("Role") != "Business")
            {
                return Unauthorized();
            }

            try
            {
                var receivedOrder = await _orderService.ReceiveOrder(order);
                return CreatedAtAction(nameof(ReceiveOrder), new { id = receivedOrder.Id }, receivedOrder);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new { ex.Message });
            }
        }
    }
}