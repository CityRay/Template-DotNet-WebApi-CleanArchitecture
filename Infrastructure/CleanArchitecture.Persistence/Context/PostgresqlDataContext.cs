using CleanArchitecture.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Persistence.Context
{
    public class PostgresqlDataContext : IdentityDbContext<AppUser>
    {
        public PostgresqlDataContext(DbContextOptions<PostgresqlDataContext> options) : base(options)
        {
        }

        public DbSet<Stock> Stocks { get; set; }
        public DbSet<StockTransaction> StockTransactions { get; set; }
        public DbSet<StockRobotTransaction> StockRobotTransactions { get; set; }
        public DbSet<Follower> Followers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 建立索引
            modelBuilder.Entity<AppUser>()
                .HasIndex(u => u.PhoneNumber)
                .IsUnique();

            modelBuilder.Entity<Stock>()
                .HasIndex(s => s.Symbol)
                .IsUnique();

            modelBuilder.Entity<AppUser>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<StockRobotTransaction>()
                .HasIndex(u => u.StrategyType);

            modelBuilder.Entity<StockRobotTransaction>()
                .HasIndex(u => u.Symbol);

            // 設定外鍵和關聯性
            modelBuilder.Entity<Follower>()
                .HasOne(f => f.User)
                .WithMany(u => u.Followers)
                .HasForeignKey(f => f.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Follower>()
                .HasOne(f => f.Stock)
                .WithMany(s => s.Followers)
                .HasForeignKey(f => f.StockId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Follower>()
               .HasIndex(f => f.UserId);

            modelBuilder.Entity<Follower>()
               .HasIndex(f => f.StockId);

            // 設定 UserId 與 StockId 組合索引
            modelBuilder.Entity<Follower>()
                .HasIndex(f => new { f.UserId, f.StockId })
                .IsUnique();

            modelBuilder.Entity<StockTransaction>()
                .HasOne(t => t.User)
                .WithMany(u => u.Transactions)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<StockTransaction>()
                .HasOne(t => t.Stock)
                .WithMany(s => s.Transactions)
                .HasForeignKey(t => t.StockId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<StockRobotTransaction>()
                .HasOne(t => t.Stock)
                .WithMany()
                .HasForeignKey(t => t.StockId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
