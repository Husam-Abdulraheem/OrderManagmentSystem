using OrderManagmentSystem.Models;

namespace OrderManagementSystem.Models.DTOFolder
{
    public class UpdateUserDTO
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? BusinessName { get; set; }
        public Address? Addresses { get; set; }
        public string? Logo { get; set; }
    }
}