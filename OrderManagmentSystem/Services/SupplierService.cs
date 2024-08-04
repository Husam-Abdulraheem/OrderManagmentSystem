using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.Interfaces;
using OrderManagementSystem.Models.DTOFolder;
using OrderManagmentSystem.Models;

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

        public async Task<Supplier> UpdateSuppler(int id, [FromForm] UpdateSupplierDTO body)
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
            existingSupplier.User.FirstName = body.User.FirstName;
            existingSupplier.User.LastName = body.User.LastName;
            existingSupplier.User.Email = body.User.Email;
            existingSupplier.User.PhoneNumber = body.User.PhoneNumber;
            existingSupplier.User.BusinessName = body.User.BusinessName;
            existingSupplier.User.PasswordHash = existingSupplier.User.PasswordHash;
            existingSupplier.User.BusinessDocument = existingSupplier.User.BusinessDocument;

            if (body.User.Addresses != null)
            {
                if (existingSupplier.User.Addresses == null)
                {
                    // if Address null Add New Address
                    existingSupplier.User.Addresses = body.User.Addresses;
                    _db.Addresses.Add(existingSupplier.User.Addresses);
                }
                else
                {
                    existingSupplier.User.Addresses.City = body.User.Addresses.City;
                    existingSupplier.User.Addresses.District = body.User.Addresses.District;
                    existingSupplier.User.Addresses.Street = body.User.Addresses.Street;
                    existingSupplier.User.Addresses.Region = body.User.Addresses.Region;
                    existingSupplier.User.Addresses.Details = body.User.Addresses.Details;
                }
            }

            // Handle image processing
            if (body.User.Logo != null)
            {
                var resizedImage = await ImageService.ResizeAndCompressImage(body.User.Logo);

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

                    existingSupplier.User.LogoUrl = "/Images/" + uniqueFileName;
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



        public async Task<object> GetOrdersForSupplier(int id)
        {
            var orders = await _db.SupplierOrders.Where(x => x.SupplierId == id).Include(x => x.SupplierOrderItems).ToListAsync();
            if (!orders.Any())
            {
                return new { Message = "You do not have any Orders" };
            }
            return orders;
        }
    }
}