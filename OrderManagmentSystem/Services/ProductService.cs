using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.Data;
using OrderManagementSystem.Data.Models;
using OrderManagementSystem.Interfaces;
using OrderManagementSystem.Models;

namespace OrderManagementSystem.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _environment;

        public ProductService(ApplicationDbContext db, IWebHostEnvironment environment)
        {

            _db = db;
            _environment = environment;
        }




        // Get all Products from Db
        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            try
            {
                var products = await _db.Products.ToListAsync();
                return products;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error when Get All Products", ex);
            }
        }




        // Get Product by Id Form Db
        public async Task<Product> GetProductById(int id)
        {
            try
            {
                var product = await _db.Products.FirstOrDefaultAsync(x => x.Id == id);
                return product;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error When Get Product By Id", ex);
            }
        }





        //Add New Product
        public async Task<Product> AddProduct([FromForm] ProductDTO product)
        {
            string imageUrl = null;
            if (product.Image != null)
            {
                var resizedImage = await ImageService.ResizeAndCompressImage(product.Image);

                if (resizedImage != null)
                {
                    string imagesFolderPath = Path.Combine(_environment.WebRootPath, "Images", "ProductImg");
                    if (!Directory.Exists(imagesFolderPath))
                        Directory.CreateDirectory(imagesFolderPath);

                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + resizedImage.FileName;
                    string filePath = Path.Combine(imagesFolderPath, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await resizedImage.CopyToAsync(fileStream);
                    }

                    imageUrl = "https://growsoft-001-site1.htempurl.com/Images/ProductImg" + uniqueFileName;
                }
            }

            var newProduct = new Product
            {
                Title = product.Title,
                Description = product.Description,
                Price = product.Price,
                StockQuantity = product.StockQuantity,
                ImageUrl = imageUrl,
                CategoryId = product.CategoryId,
                SupplierId = product.SupplierId,
            };

            await _db.Products.AddAsync(newProduct);
            await _db.SaveChangesAsync();

            return newProduct;
        }










        // Update Product
        public async Task<Product> UpdateProduct(int id, [FromForm] ProductDTO product)
        {
            var updatedProduct = await _db.Products.Where(x => x.Id == id).FirstOrDefaultAsync();

            if (updatedProduct == null)
            {
                throw new ArgumentException("Product not found");
            }

            // Update product details
            updatedProduct.Title = product.Title;
            updatedProduct.Description = product.Description;
            updatedProduct.Price = product.Price;
            updatedProduct.CategoryId = product.CategoryId;
            updatedProduct.SupplierId = product.SupplierId;
            updatedProduct.StockQuantity = product.StockQuantity;

            // Handle image processing
            if (product.Image != null)
            {
                var resizedImage = await ImageService.ResizeAndCompressImage(product.Image);

                if (resizedImage != null)
                {
                    string imagesFolderPath = Path.Combine(_environment.WebRootPath, "Images", "ProductImg");
                    if (!Directory.Exists(imagesFolderPath))
                        Directory.CreateDirectory(imagesFolderPath);

                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + resizedImage.FileName;
                    string filePath = Path.Combine(imagesFolderPath, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await resizedImage.CopyToAsync(fileStream);
                    }

                    updatedProduct.ImageUrl = "https://growsoft-001-site1.htempurl.com/Images/ProductImg/" + uniqueFileName;
                }
                else
                {
                    updatedProduct.ImageUrl = null; // If image processing fails, set logo to null
                }
            }
            else
            {
                updatedProduct.ImageUrl = updatedProduct.ImageUrl;
            }

            _db.Products.Update(updatedProduct);
            await _db.SaveChangesAsync();

            return updatedProduct;
        }


        // Delete Products method
        public async Task<Product> DeleteProduct(int productId)
        {
            var currProduct = await _db.Products.FindAsync(productId);


            if (currProduct == null)
            {
                throw new ArgumentException("Can't find product");
            }

            _db.Products.Remove(currProduct);
            await _db.SaveChangesAsync();
            return currProduct;

        }

    }
}
