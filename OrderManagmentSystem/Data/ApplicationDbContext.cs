using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.Data.Models;

namespace OrderManagementSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=SQL8020.site4now.net;Initial Catalog=db_aaaa68_orderonline;User Id=db_aaaa68_orderonline_admin;Password=Enoah90(smarter)");
        }




        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Retailer> Retailers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Name = "Supplier",
                    NormalizedName = "SUPPLIER"
                },
                new IdentityRole
                {
                    Name = "Retailer",
                    NormalizedName = "RETAILER"
                },
            };
            builder.Entity<IdentityRole>().HasData(roles);

            builder.Entity<IdentityUserLogin<string>>()
            .HasKey(x => new { x.LoginProvider, x.ProviderKey });

            builder.Entity<Order>()
          .HasMany(o => o.OrderItems)
          .WithOne(oi => oi.Order)
          .HasForeignKey(oi => oi.OrderId)
          .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<OrderItem>()
            .HasOne(oi => oi.Product)
            .WithMany(p => p.OrderItems)
            .HasForeignKey(oi => oi.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Order>()
            .HasMany(o => o.OrderItems)
            .WithOne(oi => oi.Order)
            .HasForeignKey(oi => oi.OrderId)
            .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
