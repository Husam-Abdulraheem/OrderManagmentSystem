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

        [HttpGet]
        public async Task<IActionResult> AddNewCategory(CategoryDTO category, IFormFile? imageFile)
        {
            var newCategory = await _categoryService.AddNewCategory(category, imageFile);

            if (newCategory == null)
            {

                return BadRequest(new { Message = "Error when create new Category" });
            }
            return Ok(new { Message = "Category added successfully" });
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

        [Route("{id}")]
        [HttpGet]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var category = await _categoryService.GetCategoryById(id);
            if (category == null)
            {
                return BadRequest(new { Message = "Not found this category" });
            }
            return Ok(category);
        }

        [Route("Supplier/{id}")]
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
