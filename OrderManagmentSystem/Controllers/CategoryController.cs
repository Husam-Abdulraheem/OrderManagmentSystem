using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.Interfaces;
using OrderManagementSystem.Models;

namespace OrderManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [Route("Add")]
        [HttpPost]
        public async Task<ActionResult> AddNewCategory([FromForm] CategoryDTO category)
        {

            if (category == null)
            {

                return BadRequest(new { Message = "Error when create new Category" });
            }
            try
            {
                var newCategory = await _categoryService.AddNewCategory(category);
                return Ok(new { Message = "Category added successfully" });

            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }

        [Route("All")]
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoryService.GetAllCategories();

            if (!categories.Any())
            {
                return BadRequest(new { Message = "There is no category" });
            }
            return Ok(categories);
        }

        [Route("{id}/Suppliers")]
        [HttpGet]
        public async Task<IActionResult> GetSuppliersByCategory(int id)
        {
            var suppliers = await _categoryService.GetSuppliersByCategory(id);

            if (suppliers == null)
            {
                return BadRequest(new { Message = "No suppliers have this category" });
            }
            return Ok(suppliers);
        }
    }
}
