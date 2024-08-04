using OrderManagementSystem.Models.DTOFolder;
using OrderManagmentSystem.Models;

namespace OrderManagementSystem.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProducts();
        Task<Product> GetProductById(int id);
        Task<Product> AddProduct(ProductDTO product);
        Task<Product> UpdateProduct(int id, ProductDTO product);
        Task<Product> DeleteProduct(int productId);
    }
}
