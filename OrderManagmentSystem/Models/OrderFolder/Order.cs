using OrderManagementSystem.Models.OrderFolder;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderManagmentSystem.Models.OrderFolder
{
    public class Order
    {
        [Required]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Status { get; set; } = "Pending";


        public DateTime OrderDate { get; set; } = DateTime.Now;

        [ForeignKey("Retailer")]
        public int RetailerId { get; set; }

        public ICollection<ProductInOrder> ProductInOrder { get; set; }
    }
}
