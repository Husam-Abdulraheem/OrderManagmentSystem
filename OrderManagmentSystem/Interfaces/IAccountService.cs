using OrderManagementSystem.Authentication;
using OrderManagementSystem.Models;

namespace OrderManagementSystem.Interfaces
{
    public interface IAccountService
    {
        Task<object> Login(UsersAuth usersAuth);
        Task<object> RegisterSupplier(RegisterDTO userData);
        Task<object> RegisterRetailer(RegisterDTO userData);

    }
}
