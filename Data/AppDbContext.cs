namespace Simple_Bank_Application.Data;

using Microsoft.EntityFrameworkCore;
using Simple_Bank_Application.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<BankAccount> BankAccounts { get; set; }
    public DbSet<Transaction> Transactions { get; set; }

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

        modelBuilder.Entity<Transaction>()
            .HasOne<BankAccount>()
            .WithMany()
            .HasForeignKey(t => t.AccountId)
            .OnDelete(DeleteBehavior.SetNull); //transactions won't be deleted

        modelBuilder.Entity<Transaction>()
            .HasOne<BankAccount>()
            .WithMany()
            .HasForeignKey(t => t.RelatedAccountId)
            .OnDelete(DeleteBehavior.Restrict); //transactions won't be deleted

        modelBuilder.Entity<BankAccount>()
            .HasIndex(b => b.UserId);

        modelBuilder.Entity<Transaction>()
            .HasIndex(t => t.AccountId);

        modelBuilder.Entity<Transaction>()
            .HasIndex(t => t.RelatedAccountId);
    }
}