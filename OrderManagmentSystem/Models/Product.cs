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
        [Range(0, float.MaxValue, ErrorMessage = "Price can not be a negative value")]
        public float Price { get; set; } = 0;

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Amount can not be a negative value")]
        public int StockQuantity { get; set; } = 0;


        [Required]
        public int CategorieId { get; set; }

        [Required]
        public int SupplierId { get; set; }

        public string? Image { get; set; }



    }
}
