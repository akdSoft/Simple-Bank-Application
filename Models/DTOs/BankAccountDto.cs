using System.ComponentModel.DataAnnotations;

namespace Simple_Bank_Application.Models.DTOs;

public class BankAccountDto
{
    public int Id { get; set; }
    public decimal Balance { get; set; }
    public string AccountType { get; set; } = null!;
    public string CurrencyType { get; set; } = null!;
    public char CurrencySymbol { get; set; }
    public int UserId { get; set; }
    public string UserName { get; set; } = null!;
    public string UserSurname { get; set; } = null!;
}

public class CreateBankAccountDto
{
    [Required]
    public string AccountType { get; set; } = null!;

    [Required]
    public int CurrencyId { get; set; }
}