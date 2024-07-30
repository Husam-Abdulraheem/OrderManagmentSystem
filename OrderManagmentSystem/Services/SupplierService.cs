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
        public SupplierService(ApplicationDbContext db)
        {
            _db = db;
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

        public async Task<Supplier> UpdateSuppler(int id, UpdateSupplierDTO body)
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
            existingSupplier.User.Logo = body.User.Logo;
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

            _db.Suppliers.Update(existingSupplier);
            await _db.SaveChangesAsync();
            return existingSupplier;
        }



        public async Task<IActionResult> GetOrdersForSupplier(int id)
        {
            var orders = _db.SupplierOrders.Where(x => x.SupplierId == id).Include(x => x.SupplierOrderItems);
            if (!orders.Any())
            {
                throw new ArgumentException("You do not have any Orders");
            }
            return orders;
        }
        //////////////////////////////////////////////////////

    }
}