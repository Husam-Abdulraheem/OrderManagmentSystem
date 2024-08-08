using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.Interfaces;
using OrderManagementSystem.Models;

namespace OrderManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierService _supplierService;
        public SupplierController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        // Get all Suppliers
        [HttpGet]
        [Route("All")]
        public async Task<ActionResult> GetAllSuppliers()
        {
            var allSuppliers = await _supplierService.GetAllSuppliers();

            if (!allSuppliers.Any())
            {
                return NotFound(new { Message = "Not Found Suppliers" });
            }
            return Ok(allSuppliers);
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<ActionResult> GetSupplierById(int id)
        {
            try
            {
                var supplier = await _supplierService.GetSupplierById(id);
                return Ok(supplier);

            }
            catch (Exception ex)
            {

                return NotFound(new { Message = $"Not Found this supplier with id => {id}" + ex });
            }
        }

        [Route("{id}/Update")]
        [HttpPut]
        public async Task<IActionResult> UpdateSuppler(int id, [FromForm] UpdateUserDTO body)
        {
            try
            {
                var currentSupplier = await _supplierService.UpdateSuppler(id, body);
                return Ok(currentSupplier);
            }
            catch (Exception ex)
            {

                return BadRequest(new { Message = $"Error when Update Supplier with id => {id}" + ex });
            }
        }
    }
}
