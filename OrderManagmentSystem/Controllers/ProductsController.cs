using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.Interfaces;
using OrderManagementSystem.Models.DTOFolder;

namespace OrderManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;


        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }


        // Get all Products 
        [Route("All")]
        [HttpGet]
        public async Task<ActionResult> GetAllProducts()
        {
            var products = await _productService.GetAllProducts();
            if (products == null)
            {
                return NotFound();
            }
            return Ok(products);
        }


        // Get Product By Id
        [Route("{id}")]
        [HttpGet]
        public async Task<ActionResult> GetProductById(int id)
        {
            var product = await _productService.GetProductById(id);
            if (product == null)
            {
                return NotFound(new { Message = "Not Found This Product" });
            }
            return Ok(product);
        }


        // Add New Product
        [Route("Add")]
        [HttpPost]
        public async Task<ActionResult> AddProduct([FromForm] ProductDTO product)
        {
            if (product == null)
            {
                return BadRequest(new { Message = "Error Can't Add this product" });
            }
            try
            {
                var addProduct = await _productService.AddProduct(product);
                return Ok(addProduct);
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }


        // Update Product
        [Route("Update/{id}")]
        [HttpPatch]
        public async Task<ActionResult> Edit(int id, [FromForm] ProductDTO product)
        {
            try
            {
                var updatedProduct = await _productService.UpdateProduct(id, product);
                return Ok(updatedProduct);
            }
            catch (Exception ex)
            {

                return StatusCode(400, ex.Message);
            }
        }


        // Delete Products
        [Route("Delete/{id}")]
        [HttpDelete]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var deleteProduct = await _productService.DeleteProduct(id);
            if (deleteProduct == null)
            {
                return NotFound(new { Message = "An error has occurred" });
            }
            return Ok(nameof(deleteProduct));
        }


    }
}
