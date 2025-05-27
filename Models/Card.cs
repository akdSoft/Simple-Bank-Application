namespace Simple_Bank_Application.Models;

public class Card
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Type { get; set; } = null!;
    public string CardNumber { get; set; } = null!;
    public DateTime ExpirationDate { get; set; }
    public string CVV { get; set; } = null!;
    public string CardholderNameAndSurname { get; set; } = null!;
    public bool OnlineShopping { get; set; }
}

public class DebitCard : Card
{
    public string Password { get; set; } = null!;
    public int LinkedAccountId { get; set; }
    public BankAccount LinkedAccount { get; set; } = null!;
}

public class VirtualCard : Card
{
    public decimal AvailableLimit { get; set; }
}
