using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.Data;
using OrderManagementSystem.Data.Models;
using OrderManagementSystem.Interfaces;
using OrderManagementSystem.Models;

namespace OrderManagementSystem.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _environment;
        public SupplierService(ApplicationDbContext db, IWebHostEnvironment environment)
        {
            _db = db;
            _environment = environment;
        }

        public async Task<IEnumerable<Supplier>> GetAllSuppliers()
        {
            var suppliers = await _db.Suppliers.Include(x => x.User).Include(x => x.Products).ToListAsync();
            if (!suppliers.Any())
            {
                throw new ArgumentException("No Suppliers");
            }
            return suppliers;
        }


        public async Task<Supplier> GetSupplierById(int id)
        {
            var supplier = await _db.Suppliers.Include(s => s.User).ThenInclude(x => x.Addresses).Include(x => x.Products)
              .FirstOrDefaultAsync(s => s.Id == id);
            if (supplier == null)
            {
                throw new ArgumentException("Error: Supplier not found");
            }
            return supplier;
        }

        public async Task<Supplier> UpdateSuppler(int id, [FromForm] UpdateUserDTO body)
        {
            var existingSupplier = await _db.Suppliers
               .Include(x => x.User)
               .ThenInclude(x => x.Addresses)
               .Where(x => x.Id == id)
               .FirstOrDefaultAsync();
            if (existingSupplier == null)
            {
                throw new ArgumentException("Supplier not found");
            }
            existingSupplier.User.FirstName = body.FirstName;
            existingSupplier.User.LastName = body.LastName;
            existingSupplier.User.Email = body.Email;
            existingSupplier.User.PhoneNumber = body.PhoneNumber;
            existingSupplier.User.BusinessName = body.BusinessName;
            existingSupplier.User.PasswordHash = existingSupplier.User.PasswordHash;
            existingSupplier.User.BusinessDocument = existingSupplier.User.BusinessDocument;

            if (body.Addresses != null)
            {
                if (existingSupplier.User.Addresses == null)
                {
                    // if Address null Add New Address
                    existingSupplier.User.Addresses = body.Addresses;
                    _db.Addresses.Add(existingSupplier.User.Addresses);
                }
                else
                {
                    existingSupplier.User.Addresses.City = body.Addresses.City;
                    existingSupplier.User.Addresses.District = body.Addresses.District;
                    existingSupplier.User.Addresses.Street = body.Addresses.Street;
                    existingSupplier.User.Addresses.Region = body.Addresses.Region;
                    existingSupplier.User.Addresses.Details = body.Addresses.Details;
                }
            }

            // Handle image processing
            if (body.Logo != null)
            {
                var resizedImage = await ImageService.ResizeAndCompressImage(body.Logo);

                if (resizedImage != null)
                {
                    string imagesFolderPath = Path.Combine(_environment.WebRootPath, "Images", "ProfileImg");
                    if (!Directory.Exists(imagesFolderPath))
                        Directory.CreateDirectory(imagesFolderPath);

                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + resizedImage.FileName;
                    string filePath = Path.Combine(imagesFolderPath, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await resizedImage.CopyToAsync(fileStream);
                    }

                    existingSupplier.User.LogoUrl = "https://growsoft-001-site1.htempurl.com/Images/ProfileImg/" + uniqueFileName;
                }
                else
                {
                    existingSupplier.User.LogoUrl = null; // If image processing fails, set logo to null
                }
            }

            _db.Suppliers.Update(existingSupplier);
            await _db.SaveChangesAsync();
            return existingSupplier;
        }

    }
}