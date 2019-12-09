using AS.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace AS.Core.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Reward> Rewards { get; set; }
        
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<Reward>()
                .HasKey(x => x.Id);
            base.OnModelCreating(modelBuilder);
        }
    }
}