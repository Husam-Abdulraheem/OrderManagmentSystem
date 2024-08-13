using OrderManagementSystem.Data.Models;

namespace OrderManagementSystem.Models
{
    public class RetailerDTO
    {
        public int RetailerId { get; set; }
        public string RetailerFirstName { get; set; }
        public string RetailerLastName { get; set; }
        public string RetailerPhoneNumber { get; set; }
        public string Logo { get; set; }
        public string BusinessName { get; set; }
        public Address RetailerAddress { get; set; }

    }
}
