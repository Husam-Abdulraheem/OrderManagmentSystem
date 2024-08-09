using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.Data;
using OrderManagementSystem.Data.Models;
using OrderManagementSystem.Interfaces;
using OrderManagementSystem.Models;

namespace OrderManagementSystem.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _environment;

        public CategoryService(ApplicationDbContext db, IWebHostEnvironment environment)
        {
            _db = db;
            _environment = environment;
        }



        public async Task<Category> AddNewCategory([FromForm] CategoryDTO categ)
        {
            string? imageUrl = null;

            if (categ.Image != null)
            {
                var resizedImage = await ImageService.ResizeAndCompressImage(categ.Image);

                if (resizedImage != null)
                {
                    string imagesFolderPath = Path.Combine(_environment.WebRootPath, "Images");
                    if (!Directory.Exists(imagesFolderPath))
                        Directory.CreateDirectory(imagesFolderPath);

                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + resizedImage.FileName;
                    string filePath = Path.Combine(imagesFolderPath, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await resizedImage.CopyToAsync(fileStream);
                    }

                    imageUrl = "/Images/" + uniqueFileName;
                }
            }

            Category category = new Category()
            {
                Name = categ.Name,
                ImageUrl = imageUrl,
                Products = new List<Product>()
            };

            await _db.Categories.AddAsync(category);
            await _db.SaveChangesAsync();

            throw new ArgumentException("Category added successfully");
        }





        // Get All Categories
        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            try
            {
                var categories = await _db.Categories.ToListAsync();
                return categories;
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Error When Get All Categories", ex);
            }
        }

        // Get All Supplier which have Same Category
        public async Task<List<Supplier>> GetSuppliersByCategory(int categoryId)
        {
            var supplier = await _db.Suppliers.Where(p => p.Products.Any(c => c.CategoryId == categoryId)).Include(u => u.User).ToListAsync();
            if (supplier == null)
            {
                throw new ApplicationException("Don't have suppliers have this category");
            }
            return supplier;
        }

        public async Task<Category> Delete(int id)
        {
            var category = await _db.Categories.FindAsync(id);
            if (category == null)
            {
                throw new ApplicationException("An error has occurred");
            }
            _db.Categories.Remove(category);
            _db.SaveChanges();
            return category;
        }
    }
}
