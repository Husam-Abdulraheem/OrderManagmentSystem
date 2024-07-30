using OrderManagementSystem.Models;
using OrderManagmentSystem.Models;

namespace OrderManagementSystem.Interfaces
{
    public interface ICategoryService
    {
        Task<Category> AddNewCategory(CategoryDTO category, IFormFile? imageFile);
        Task<IEnumerable<Category>> GetAllCategories();
        Task<Category> GetCategoryById(int id);
        Task<List<Supplier>> GetSuppliersByCategory(int categoryId);
    }
}
