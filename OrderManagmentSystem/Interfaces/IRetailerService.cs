using OrderManagementSystem.Data.Models;
using OrderManagementSystem.Models;

namespace OrderManagementSystem.Interfaces
{
    public interface IRetailerService
    {
        Task<Retailer> GetRetailerById(int id);
        Task<Retailer> UpdateRetailer(int id, UpdateUserDTO retailerDTO);
    }
}
