using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderManagmentSystem.Models
{
    public class Supplier
    {
        [Required]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        public int UserId { get; set; }
        [Required]
        public User User { get; set; }

        //public ICollection<Order> Order { get; set; }

        public ICollection<Product> Products { get; set; }

        public int categoryId { get; set; }

        public string? subscription { get; set; }
    }
}