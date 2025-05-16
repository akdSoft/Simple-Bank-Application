using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Simple_Bank_Application.Services.Interfaces;

namespace Simple_Bank_Application.Pages.customer
{
    public class transfer_moneyModel : PageModel
    {
        private readonly ITransactionService _transactionService;
        private readonly IBankAccountService _bankAccountService;
        public transfer_moneyModel(ITransactionService transactionService, IBankAccountService bankAccountService)
        {
            _transactionService = transactionService;
            _bankAccountService = bankAccountService;
        }
        [BindProperty]
        public int? SelectedAccountId { get; set; }

        [BindProperty]
        public int? TargetAccountId { get; set; }

        [BindProperty]
        public decimal Balance { get; set; } = 0.00m;

        [BindProperty]
        public int? Amount { get; set; }
        public List<SelectListItem> Accounts { get; set; } = new List<SelectListItem>();
        public async Task OnGet()
        {
            await UpdateAccounts();
        }
        public async Task OnPostUpdate()
        {
            await UpdateBalance();
            await UpdateAccounts();
        }

        public async Task OnPostTransfer(int targetAccountId, int amount)
        {
            Console.WriteLine("target id: " + targetAccountId + "   amount: " + amount);
            await UpdateBalance();
            await UpdateAccounts();
        }

        private async Task UpdateAccounts()
        {
            var accounts = await _bankAccountService.GetBankAccountsByUserId((int)HttpContext.Session.GetInt32("Id"));

            Accounts = accounts.Select(acc => new SelectListItem
            {
                Value = acc.Id.ToString(),
                Text = $"{acc.Id}"
            }).ToList();
        }
        private async Task UpdateBalance()
        {
            if(SelectedAccountId != null)
            {
                var account = await _bankAccountService.GetBankAccountByIdAsync((int)SelectedAccountId);
                Balance = account.Balance;
            }
            else
            {
                Balance = 0.00m;
            }
        }
    }
}
