namespace Simple_Bank_Application.Models.DTOs;

public class CreateCardDto
{}

public class CreateDebitCardDto
{
    public bool OnlineShopping { get; set; }
    public string Password { get; set; } = null!;
    public int LinkedAccountId { get; set; }
}

public class CreateVirtualCardDto
{
    public bool OnlineShopping { get; set; }
}