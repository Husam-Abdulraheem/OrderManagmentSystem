using OrderManagementSystem.Data.Models;
using OrderManagementSystem.Models;

namespace OrderManagementSystem.Interfaces
{
    public interface ICategoryService
    {
        Task<Category> AddNewCategory(CategoryDTO categ);
        Task<IEnumerable<Category>> GetAllCategories();
        Task<List<Supplier>> GetSuppliersByCategory(int categoryId);
        Task<Category> Delete(int id);
    }
}
