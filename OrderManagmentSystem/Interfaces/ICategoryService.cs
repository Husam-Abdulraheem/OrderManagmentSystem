using OrderManagementSystem.Models.DTOFolder;
using OrderManagmentSystem.Models;

namespace OrderManagementSystem.Interfaces
{
    public interface ICategoryService
    {
        Task<Category> AddNewCategory(CategoryDTO categ);
        Task<IEnumerable<Category>> GetAllCategories();
        Task<Category> GetCategoryById(int id);
        Task<List<Supplier>> GetSuppliersByCategory(int categoryId);
        Task<Category> Delete(int id);
    }
}
