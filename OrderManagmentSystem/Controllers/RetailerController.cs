using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.Interfaces;
using OrderManagementSystem.Models;

namespace OrderManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RetailerController : ControllerBase
    {
        private readonly IRetailerService _retailerService;
        public RetailerController(IRetailerService retailerService)
        {
            _retailerService = retailerService;
        }


        [Route("{id}")]
        [HttpGet]
        public async Task<ActionResult> GetRetailerById(int id)
        {
            try
            {
                var supplier = await _retailerService.GetRetailerById(id);
                return Ok(supplier);

            }
            catch (Exception ex)
            {

                return NotFound(new { Message = $"Not Found this retailer with id => {id}" + ex });
            }
        }


        [Route("{id}/Update")]
        [HttpPut]
        public async Task<IActionResult> UpdateRetailer(int id, [FromForm] UpdateUserDTO body)
        {
            try
            {
                var retailer = await _retailerService.UpdateRetailer(id, body);
                return Ok(retailer);
            }
            catch (Exception ex)
            {

                return BadRequest(new { Message = $"Error when Update Supplier with id => {id}" + ex });
            }
        }
    }
}
