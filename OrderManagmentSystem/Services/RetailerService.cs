using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.Data;
using OrderManagementSystem.Data.Models;
using OrderManagementSystem.Interfaces;
using OrderManagementSystem.Models;

namespace OrderManagementSystem.Services
{
    public class RetailerService : IRetailerService
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _environment;
        public RetailerService(ApplicationDbContext db, IWebHostEnvironment environment)
        {
            _db = db;
            _environment = environment;
        }

        public async Task<Retailer> GetRetailerById(int id)
        {
            var retailer = await _db.Retailers.Include(s => s.User).ThenInclude(x => x.Addresses)
                .FirstOrDefaultAsync(s => s.Id == id);
            if (retailer == null)
            {
                throw new ArgumentException("Error: Retailer not found");
            }
            return retailer;
        }


        public async Task<Retailer> UpdateRetailer(int id, [FromForm] UpdateUserDTO retailerDTO)
        {
            var retailer = await _db.Retailers
              .Include(x => x.User)
              .ThenInclude(x => x.Addresses)
              .Where(x => x.Id == id)
              .FirstOrDefaultAsync();
            if (retailer == null)
            {
                throw new ArgumentException("Retailer not found");
            }
            retailer.User.FirstName = retailerDTO.FirstName;
            retailer.User.LastName = retailerDTO.LastName;
            retailer.User.Email = retailerDTO.Email;
            retailer.User.PhoneNumber = retailerDTO.PhoneNumber;
            retailer.User.BusinessName = retailerDTO.BusinessName;
            retailer.User.PasswordHash = retailer.User.PasswordHash;
            retailer.User.BusinessDocument = retailer.User.BusinessDocument;

            if (retailerDTO.Addresses != null)
            {
                if (retailer.User.Addresses == null)
                {
                    // if Address null Add New Address
                    retailer.User.Addresses = retailerDTO.Addresses;
                    _db.Addresses.Add(retailer.User.Addresses);
                }
                else
                {
                    retailer.User.Addresses.City = retailerDTO.Addresses.City;
                    retailer.User.Addresses.District = retailerDTO.Addresses.District;
                    retailer.User.Addresses.Street = retailerDTO.Addresses.Street;
                    retailer.User.Addresses.Region = retailerDTO.Addresses.Region;
                    retailer.User.Addresses.Details = retailerDTO.Addresses.Details;
                }
            }

            // Handle image processing
            if (retailerDTO.Logo != null)
            {
                var resizedImage = await ImageService.ResizeAndCompressImage(retailerDTO.Logo);

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

                    retailer.User.LogoUrl = "https://husamta-001-site1.htempurl.com/Images/" + uniqueFileName;
                }
                else
                {
                    retailer.User.LogoUrl = null; // If image processing fails, set logo to null
                }
            }

            _db.Retailers.Update(retailer);
            await _db.SaveChangesAsync();
            return retailer;
        }


    }
}
