using OrderManagementSystem.Data.Models;

namespace OrderManagementSystem.Models
{
    public class UpdateUserDTO
    {
        public int UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? BusinessName { get; set; }
        public virtual Address? Addresses { get; set; }
        public IFormFile? Logo { get; set; }
    }
}