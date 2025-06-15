namespace Simple_Bank_Application.Models;

public class Currency
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public decimal TryIndexedValue { get; set; }
    public char Symbol { get; set; }
    public int ISO4217Code { get; set; }
}
