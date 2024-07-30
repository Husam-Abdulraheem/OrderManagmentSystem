using OrderManagmentSystem.Models.OrderFolder;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderManagmentSystem.Models
{
    public class Supplier
    {
        [Required]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public User User { get; set; }

        public ICollection<Order> Order { get; set; }

        public ICollection<Product> Products { get; set; }

        public int CategoryId { get; set; }

        public string? Subscription { get; set; }
    }
}