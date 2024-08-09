namespace OrderManagementSystem.Models
{
    public class OrderItemDTO
    {
        public int ProductId { get; set; }
        public virtual ProductDTO? Product { get; set; }
        public int SupplierId { get; set; }
        public int Quantity { get; set; }
    }
}
