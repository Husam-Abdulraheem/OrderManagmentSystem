namespace OrderManagementSystem.Models
{
    public class OrderDTO
    {
        public int OrderId { get; set; }
        public int RetailerId { get; set; }
        public virtual RetailerDTO? Retailer { get; set; }
        public DateTime OrderDate { get; set; }
        public virtual List<OrderItemDTO>? Items { get; set; } = new List<OrderItemDTO>();
    }
}
