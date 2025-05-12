namespace Simple_Bank_Application.Models.DTOs;

public class BankAccountDto
{
    public int Id { get; set; }
    public decimal Balance { get; set; }
    public int UserId { get; set; }
    public string UserName { get; set; } = null!;
    public string UserSurname { get; set; } = null!;
}

public class CreateBankAccountDto
{
    //public int UserId { get; set; }
}
