using System.Text.Json.Serialization;

namespace Simple_Bank_Application.Models;

public class BankAccount
{
    public int Id { get; set; }
    public decimal Balance { get; set; }
    public string AccountType { get; set; } = null!;
    public int CurrencyId { get; set; }
    public Currency Currency { get; set; } = null!;
    public int UserId { get; set; }
    public User User { get; set; } = null!;

    [JsonIgnore]
    public ICollection<DebitCard> DebitCards { get; } = new List<DebitCard>();
}
