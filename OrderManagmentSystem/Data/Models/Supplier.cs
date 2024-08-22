using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderManagementSystem.Data.Models
{
    public class Supplier
    {
        [Required]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public virtual User User { get; set; }

        public virtual ICollection<Product>? Products { get; set; }

        public Subscription? Subscription { get; set; }
    }
}