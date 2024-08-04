using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderManagementSystem.Models.OrderFolder
{
    public class ProductInOrder
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ProductId { get; set; }

        public int Quantity { get; set; }

        [ForeignKey("Supplier")]
        public int SupplierId { get; set; }
    }
}
