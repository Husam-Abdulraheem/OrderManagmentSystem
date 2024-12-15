using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderManagementSystem.Data.Models
{
    public class Order
    {
        [Required]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTime OrderDate { get; set; }

        public float TotalDeal { get; set; }

        public string State { get; set; }

        [ForeignKey("Retailer")]
        public int RetailerId { get; set; }

        public virtual Retailer Retailer { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
