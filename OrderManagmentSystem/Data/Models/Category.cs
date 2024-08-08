using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderManagementSystem.Data.Models
{
    [Table("CategoryTable")]
    public class Category
    {
        [Required]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MinLength(2), StringLength(50)]
        public string Name { get; set; }

        public virtual ICollection<Product>? Products { get; set; }

        public string? ImageUrl { get; set; }
    }
}