namespace Simple_Bank_Application.Data;

using Microsoft.EntityFrameworkCore;
using Simple_Bank_Application.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<BankAccount> BankAccounts { get; set; }
}
