namespace OrderManagementSystem.Data.Models
{
    public class Subscription
    {
        public int Id { get; set; }
        public int SupplierId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? Type { get; set; }
        public bool IsActive { get; set; }
        public Supplier? Supplier { get; set; }
    }
}
