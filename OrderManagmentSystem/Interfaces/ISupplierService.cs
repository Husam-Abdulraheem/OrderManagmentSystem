using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.Models.DTOFolder;
using OrderManagmentSystem.Models;

namespace OrderManagementSystem.Interfaces
{
    public interface ISupplierService
    {
        Task<IEnumerable<Supplier>> GetAllSuppliers();
        Task<Supplier> GetSupplierById(int id);
        Task<Supplier> UpdateSuppler(int id, UpdateSupplierDTO body);
        Task<IActionResult> GetOrdersForSupplier(int id);
    }
}
