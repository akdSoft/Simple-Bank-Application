using System.ComponentModel.DataAnnotations;

namespace Simple_Bank_Application.Models.DTOs;

public class UserDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;

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
    [StringLength(50)]
    public string Username { get; set; } = null!;

    [Required]
    [StringLength(50)]
    public string Password { get; set; } = null!;

    [Required]
    [StringLength(50)]
    public string Email { get; set; } = null!;
}

public class LoginDto
{
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
}