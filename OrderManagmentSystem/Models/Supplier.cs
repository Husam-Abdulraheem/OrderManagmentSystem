using System.ComponentModel.DataAnnotations;

namespace OrderManagmentSystem.Models
{
    public class Supplier
    {
        [Key]
        public long Id { get; set; } = DateTime.UtcNow.Ticks;


        public int UserId { get; set; }
        [Required]
        public User User { get; set; }

        //public ICollection<Order> Order { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}