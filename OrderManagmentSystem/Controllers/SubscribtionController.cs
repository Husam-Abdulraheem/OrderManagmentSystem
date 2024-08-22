using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.Interfaces;

namespace OrderManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscribtionController : ControllerBase
    {
        private readonly ISubscribtionService _subscribtionService;

        public SubscribtionController(ISubscribtionService subscribtionService)
        {
            _subscribtionService = subscribtionService;
        }

        [Route("IsActive/{supplierId}")]
        [HttpGet]
        public async Task<IActionResult> IsActive(int supplierId)
        {
            var isActive = await _subscribtionService.IsActive(supplierId);
            if (isActive == null)
                return NotFound();

            return Ok(isActive);
        }

        [Route("ReNewSubscribtion")]
        [HttpPost]
        public async Task<IActionResult> RenewSubscription(int supplierId, string type)
        {
            await _subscribtionService.RenewSubscription(supplierId, type);
            return Ok();
        }


    }
}
