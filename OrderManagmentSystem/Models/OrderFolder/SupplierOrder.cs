using OrderManagmentSystem.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderManagementSystem.Models.OrderFolder
{
    public class SupplierOrder
    {
        public int Id { get; set; }

        [ForeignKey("Supplier")]
        public int SupplierId { get; set; }
        public DateTime OrderDate { get; set; }

        public float Total { get; set; }

        [ForeignKey("Retailer")]
        public int RetailerId { get; set; }
        public Retailer Retailer { get; set; }

        public List<SupplierOrderItem> SupplierOrderItems { get; set; }
    }
}
