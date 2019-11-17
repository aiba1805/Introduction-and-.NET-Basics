using EFCore.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCore
{
    public class AppContext: DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Seller> Sellers { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Cart> Carts { get; set; }

        public AppContext() : base()
        {
            Database.EnsureDeleted();
                
            Database.EnsureCreated ();
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseLazyLoadingProxies().UseSqlite("Filename=./shop.sqlite");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cart>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<Comment>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<Seller>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<Customer>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<Order>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<Product>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<ProductsCarts>()
                .HasKey(t => new {t.CartId, t.ProductId});

            modelBuilder.Entity<ProductsCarts>()
                .HasOne(t => t.Product)
                .WithMany(p => p.ProductsCarts)
                .HasForeignKey(pc=>pc.ProductId);
            
            modelBuilder.Entity<ProductsCarts>()
                .HasOne(t => t.Cart)
                .WithMany(p => p.ProductsCarts)
                .HasForeignKey(pc=>pc.CartId);
            
            base.OnModelCreating(modelBuilder);
        }
    }
}