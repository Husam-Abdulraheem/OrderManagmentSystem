namespace OrderManagementSystem.Models
{
    public class ProductDTO
    {
        public int ProductId { get; set; }
        public string Title { get; set; }

        public string? Description { get; set; }

        public float Price { get; set; } = 0;

        public int StockQuantity { get; set; } = 0;

        public int CategoryId { get; set; }

        public int SupplierId { get; set; }

        public IFormFile? Image { get; set; }
        public string? viewImage { get; set; }
    }
}
