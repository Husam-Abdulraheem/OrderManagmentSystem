using OrderManagmentSystem.Models;

namespace OrderManagementSystem.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProducts();
        Task<Product> GetProductById(int id);
        Task<Product> AddProduct(Product product, IFormFile? imageFile);
        Task<Product> UpdateProduct(int id, Product product, IFormFile? imageFile);
        Task DeleteProduct(int id);
    }
}
