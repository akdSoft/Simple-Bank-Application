namespace Simple_Bank_Application.Models.DTOs;

public class CreateCardDto
{}

public class CreateDebitCardDto
{
    public string Type { get; set; } = null!;
    public bool OnlineShopping { get; set; }
    public string Password { get; set; } = null!;
    public int LinkedAccountId { get; set; }
}

public class CreateVirtualCardDto
{
    public string Type { get; set; } = null!;
    public bool OnlineShopping { get; set; }

}
