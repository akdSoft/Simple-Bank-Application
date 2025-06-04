using System.ComponentModel.DataAnnotations;

namespace Simple_Bank_Application.Models.DTOs;

public class CreateCardDto
{}

public class CreateDebitCardDto
{
    [Required]
    public bool OnlineShopping { get; set; }

    [Required]
    public string Password { get; set; } = null!;

    [Required]
    public int LinkedAccountId { get; set; }
}

public class CreateVirtualCardDto
{
    [Required]
    public bool OnlineShopping { get; set; }
}