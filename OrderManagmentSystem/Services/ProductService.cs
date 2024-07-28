using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.Interfaces;
using OrderManagmentSystem.Models;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Processing;


namespace OrderManagementSystem.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _db;

        public ProductService(ApplicationDbContext db)
        {

            _db = db;
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
        public async Task<Product> AddProduct(Product product, IFormFile? imageFile)
        {
            if (product == null)
            {
                throw new ArgumentException("Product cannot be null.");
            }

            if (imageFile == null || imageFile.Length == 0)
            {
                throw new ArgumentException("Invalid image file.");
            }

            var validExtensions = new[] { ".jpg", ".jpeg", ".png", ".svg" };
            var fileExtension = Path.GetExtension(imageFile.FileName).ToLowerInvariant();

            if (!validExtensions.Contains(fileExtension))
            {
                throw new ArgumentException("Invalid image file format.");
            }

            string fileName = Path.GetFileNameWithoutExtension(imageFile.FileName) + fileExtension;
            string directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images");
            string filePath = Path.Combine(directoryPath, fileName);

            // Ensure the directory exists
            Directory.CreateDirectory(directoryPath);

            try
            {
                using (var stream = imageFile.OpenReadStream())
                {
                    using (var image = await Image.LoadAsync(stream))
                    {
                        // Resize Image
                        image.Mutate(x => x.Resize(new ResizeOptions
                        {
                            Mode = ResizeMode.Max,
                            Size = new Size(400, 400)
                        }));

                        IImageEncoder encoder = fileExtension switch
                        {
                            ".jpg" or ".jpeg" => new JpegEncoder { Quality = 75 },
                            ".png" => new PngEncoder(),
                            // Add other encoders if needed
                            _ => throw new NotSupportedException($"File extension {fileExtension} is not supported.")
                        };

                        await image.SaveAsync(filePath, encoder);
                    }
                }

                var newProduct = new Product
                {
                    Title = product.Title,
                    Description = product.Description,
                    Price = product.Price,
                    StockQuantity = product.StockQuantity,
                    Image = "wwwroot/Images/" + fileName,
                    CategorieId = product.CategorieId,
                    SupplierId = product.SupplierId,
                };

                _db.Products.Add(newProduct);
                _db.SaveChanges();

                return newProduct;
            }
            catch (NotSupportedException nsEx)
            {
                throw new ArgumentException(nsEx.Message, nsEx);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while saving the image file.", ex);
            }
        }








        // Update Product
        public async Task<Product> UpdateProduct(int id, Product product, IFormFile? imageFile)
        {
            var updatedProduct = await _db.Products.Where(x => x.Id == id).FirstOrDefaultAsync();


            if (product == null)
            {
                throw new ArgumentException("Product cannot be null.");
            }

            if (imageFile == null || imageFile.Length == 0)
            {
                throw new ArgumentException("Invalid image file.");
            }

            var validExtensions = new[] { ".jpg", ".jpeg", ".png", ".svg" };
            var fileExtension = Path.GetExtension(imageFile.FileName).ToLowerInvariant();

            if (!Array.Exists(validExtensions, ext => ext == fileExtension))
            {
                throw new ArgumentException("Invalid image file format.");
            }

            string fileName = Path.GetFileNameWithoutExtension(imageFile.FileName) + fileExtension;
            string directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "Images");
            string filePath = Path.Combine(directoryPath, fileName);

            // Ensure the directory exists
            Directory.CreateDirectory(directoryPath);

            try
            {
                using (var image = await Image.LoadAsync(imageFile.OpenReadStream()))
                {
                    // Resize Image
                    image.Mutate(x => x.Resize(new ResizeOptions
                    {
                        Mode = ResizeMode.Max,
                        Size = new Size(400, 400)
                    }));

                    var encoder = new JpegEncoder
                    {
                        Quality = 75
                    };

                    await image.SaveAsync(filePath, encoder);
                }

                updatedProduct.Title = product.Title;
                updatedProduct.Description = product.Description;

                _db.Products.Update(updatedProduct);
                await _db.SaveChangesAsync();

                return updatedProduct;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while saving the image file.", ex);
            }
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
