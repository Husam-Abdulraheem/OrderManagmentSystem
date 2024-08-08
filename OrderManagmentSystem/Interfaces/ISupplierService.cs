using OrderManagementSystem.Data.Models;
using OrderManagementSystem.Models;

namespace OrderManagementSystem.Interfaces
{
    public interface ISupplierService
    {
        Task<IEnumerable<Supplier>> GetAllSuppliers();
        Task<Supplier> GetSupplierById(int id);
        Task<Supplier> UpdateSuppler(int id, UpdateUserDTO body);
    }
}
