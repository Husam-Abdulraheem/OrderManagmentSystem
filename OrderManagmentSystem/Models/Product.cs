using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderManagmentSystem.Models
{
    [Table("ProductTable")]
    public class Product
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string? Description { get; set; }

        [Required]
        public float Price { get; set; } = 0;

        [Required]
        public int StockQuantity { get; set; } = 0;


        [Required]
        public int CategoryId { get; set; }

        [Required]
        public int SupplierId { get; set; }

        public string? ImageUrl { get; set; }



    }
}
