namespace Simple_Bank_Application.Data;

using Microsoft.EntityFrameworkCore;
using Simple_Bank_Application.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<BankAccount> BankAccounts { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<DebitCard> DebitCards { get; set; }
    public DbSet<VirtualCard> VirtualCards { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
            .HasIndex(u => u.Username)
            .IsUnique();

        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

        modelBuilder.Entity<BankAccount>()
            .HasOne(b => b.User)
            .WithMany(u => u.BankAccounts)
            .HasForeignKey(b => b.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<BankAccount>()
            .HasIndex(b => b.UserId);

        modelBuilder.Entity<DebitCard>()
            .HasOne<BankAccount>()
            .WithMany()
            .HasForeignKey("LinkedAccountId")
            .OnDelete(DeleteBehavior.Cascade);
    }
}