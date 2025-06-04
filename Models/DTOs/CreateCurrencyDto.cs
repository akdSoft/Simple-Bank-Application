using System.ComponentModel.DataAnnotations;

namespace Simple_Bank_Application.Models.DTOs;

public class CreateCurrencyDto
{
    [Required]
    public string Name { get; set; } = null!;

    [Required]
    public decimal TRYIndexedValue { get; set; }

    [Required]
    public char Symbol { get; set; }
}
