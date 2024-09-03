using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.Interfaces;

namespace OrderManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionController : ControllerBase
    {
        private readonly ISubscriptionService _subscriptionService;

        public SubscriptionController(ISubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }

        [Route("IsActive/{supplierId}")]
        [HttpGet]
        public async Task<IActionResult> IsActive(int supplierId)
        {
            var isActive = await _subscriptionService.IsActive(supplierId);
            if (isActive == null)
                return NotFound();

            return Ok(isActive);
        }

        [Route("AddNewSubscribtion")]
        [HttpPost]
        public async Task<IActionResult> AddNewSubscription(string supplierEmail, string type)
        {
            try
            {
                var result = await _subscriptionService.AddNewSubscription(supplierEmail, type);

                if (result == null)
                {
                    return NotFound(new { Message = "Supplier not found or an error occurred" });
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(400, new { Message = "An error occurred while Add new subscription or wrong type of subscription " + ex });
            }
        }

        [Route("Update")]
        [HttpGet]
        public async Task<IActionResult> UpdateSubscription(string email)
        {
            try
            {
                var supplier = await _subscriptionService.UpdateSubscription(email);
                if (supplier == null)
                    return BadRequest();

                return Ok(supplier);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        [Route("Delete")]
        [HttpDelete]
        public async Task<IActionResult> DeleteSubscription(string email)
        {
            try
            {
                var supplier = await _subscriptionService.DeleteSubscription(email);
                if (supplier == null)
                    return NotFound();

                return Ok(supplier);
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }



    }
}
