using OrderManagementSystem.Data.Models;
using System.Text.Json.Serialization;

namespace OrderManagementSystem.Models
{
    public class OrderDTO
    {
        public int OrderId { get; set; }
        public int RetailerId { get; set; }
        [JsonIgnore]
        public virtual Retailer? Retailer { get; set; }
        public DateTime OrderDate { get; set; }
        public virtual List<OrderItemDTO>? Items { get; set; } = new List<OrderItemDTO>();
    }
}
