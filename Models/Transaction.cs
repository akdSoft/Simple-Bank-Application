namespace Simple_Bank_Application.Models;

public class Transaction
{
    public int Id { get; set; }
    public int? AccountId { get; set; }
    public decimal Amount { get; set; }
    public string Type { get; set; } = null!;
    public int RelatedAccountId { get; set; }
}
