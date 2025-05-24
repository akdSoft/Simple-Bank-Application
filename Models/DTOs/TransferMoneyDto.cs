namespace Simple_Bank_Application.Models.DTOs
{
    public class TransferMoneyDto
    {
        public int FromAccountId { get; set; }
        public int TargetAccountId { get; set; }
        public decimal Amount { get; set; }
    }
}
