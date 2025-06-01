namespace Simple_Bank_Application.Models;

public class Transaction
{
    public int Id { get; set; }
    public string? SourceType { get; set; }
    public int? SourceId { get; set; }
    public string? TargetType { get; set; }
    public int? TargetId { get; set; }
    public decimal Amount { get; set; }
    public int UserId { get; set; }
    public string Type { get; set; } = null!;
    public DateTime Timestamp { get; set; }
}

public enum TransactionEntityType
{
    Account,
    VirtualCard,
    DebitCard
}
