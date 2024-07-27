using Microsoft.EntityFrameworkCore;

namespace OrderManagmentSystem.Models
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        //public DbSet<Retailer> Retailers { get; set; }
        public DbSet<Product> Products { get; set; }
        //public DbSet<Address> Addresses { get; set; }
        //public DbSet<Order> Orders { get; set; }
        //public DbSet<Category> Categories { get; set; }
    }
}
