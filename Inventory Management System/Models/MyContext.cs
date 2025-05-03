using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Inventory_Management_System.Models
{
    public class MyContext: IdentityDbContext<ApplicationUser>
    {
   

            public MyContext(DbContextOptions<MyContext> options) : base(options)
            {

            }

        public DbSet<Product> Products { get; set; }
        public DbSet<Stocktaking> Stocktakings { get; set; }
        public DbSet<InventoryTransaction> InventoryTransactions { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<Notification> Notifications { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Stocktaking>()
                .HasKey(p => new { p.ProductId, p.WarehouseId });

            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasPrecision(18, 2);
        }

    }
}
