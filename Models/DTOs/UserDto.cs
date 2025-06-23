using System.ComponentModel.DataAnnotations;

namespace Simple_Bank_Application.Models.DTOs;

public class UserDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public decimal TotalBalanceInTRY { get; set; }
    public List<BankAccountDto> BankAccounts { get; set; } = new List<BankAccountDto>();
}

public class CreateUserDto
{
    [Required]
    [StringLength(50)]
    public string Name { get; set; } = null!;

    [Required]
    [StringLength(50)]
    public string Surname { get; set; } = null!;

    [Required]
    [StringLength(50, MinimumLength = 4)]
    public string Username { get; set; } = null!;

    [Required]
    [StringLength(50, MinimumLength = 8)]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;
}