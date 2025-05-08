namespace Simple_Bank_Application.Models;

public class BankAccount
{
    public int Id { get; set; }
    public int Balance { get; set; }

    public int UserId { get; set; }
    public User User { get; set; } = null!;
}
