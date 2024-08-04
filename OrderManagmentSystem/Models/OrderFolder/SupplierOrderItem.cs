namespace OrderManagementSystem.Models.OrderFolder
{
    public class SupplierOrderItem
    {
        public int Id { get; set; }
        public int SupplierOrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public float TotalPrice { get; set; }
    }
}