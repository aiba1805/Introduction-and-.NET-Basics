using System;
using AS.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AS.Core.Data
{
    public class ApplicationContext : IdentityDbContext<User,IdentityRole<Guid>,Guid>
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Reward> Rewards { get; set; }

        public DbSet<UserReward> UserRewards { get; set; }
        
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=AS;Username=postgres;Password=postgres");
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Reward>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<UserReward>()
                .HasKey(t => new {t.UserId, t.RewardId});

            modelBuilder.Entity<UserReward>()
                .HasOne(t => t.User)
                .WithMany(p => p.Rewards)
                .HasForeignKey(pc=>pc.UserId);
            
            modelBuilder.Entity<UserReward>()
                .HasOne(t => t.Reward)
                .WithMany(p => p.Users)
                .HasForeignKey(pc=>pc.RewardId);
            
            base.OnModelCreating(modelBuilder);
        }
    }
}