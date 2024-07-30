using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.Interfaces;
using OrderManagementSystem.Models;
using OrderManagmentSystem.Models;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Processing;

namespace OrderManagementSystem.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _db;

        public CategoryService(ApplicationDbContext db)
        {
            _db = db;
        }



        public async Task<Category> AddNewCategory(CategoryDTO category, IFormFile? imageFile)
        {
            if (category == null)
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
            string directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/CategoryImg");
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

                var newCategory = new Category
                {
                    Name = category.Name,
                    Image = "wwwroot/Images/CategoryImg/" + fileName,
                    Products = new List<Product>()
                };

                _db.Categories.Add(newCategory);
                _db.SaveChanges();

                return newCategory;

            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while saving the image file.", ex);
            }
        }

        // Get All Categories
        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            try
            {
                var categories = await _db.Categories.Include(x => x.Products).ToListAsync();
                return categories;
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Error When Get All Categories", ex);
            }
        }

        // Get Category By Id
        public async Task<Category> GetCategoryById(int id)
        {
            var categoriesById = await _db.Categories.Include(x => x.Products).FirstOrDefaultAsync(x => x.Id == id);

            if (categoriesById == null)
            {
                throw new ApplicationException("Can't find this category");
            }
            return categoriesById;
        }

        // Get All Supplier which have Same Category
        public async Task<List<Supplier>> GetSuppliersByCategory(int categoryId)
        {
            var suppliers = await _db.Suppliers.Where(x => x.CategoryId == categoryId).Include(x => x.Products).Include(x => x.User).ToListAsync();
            if (suppliers == null)
            {
                throw new ApplicationException("Don't have suppliers have this category");
            }
            return suppliers;
        }
    }
}
