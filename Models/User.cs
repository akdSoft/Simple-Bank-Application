using System.Text.Json.Serialization;

namespace Simple_Bank_Application.Models;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Email { get; set; } = null!;
    public decimal TotalBalanceInTRY { get; set; }

    //User'ı dto ile döndürmediğimiz senaryoda json sonsuz döngüye giriyordu
    //Geçici olarak BankAccounts'a JsonIgnore ekledik
    [JsonIgnore]
    public ICollection<BankAccount> BankAccounts { get; } = new List<BankAccount>();

}
