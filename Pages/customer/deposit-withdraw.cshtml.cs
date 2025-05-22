using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Simple_Bank_Application.Services.Interfaces;

namespace Simple_Bank_Application.Pages.customer
{
    public class deposit_withdrawModel : PageModel
    {
        private readonly IBankAccountService _bankAccountService;
        private readonly ITransactionService _transactionService;

        public deposit_withdrawModel(IBankAccountService bankAccountService, ITransactionService transactionService)
        {
            _bankAccountService = bankAccountService;
            _transactionService = transactionService;
        }
        [BindProperty]
        public int? SelectedAccountId { get; set; }
        public decimal Balance { get; set; } = 0.00m;
        [BindProperty]
        public decimal? Amount { get; set; }
        public List<SelectListItem> BankAccounts { get; set; } = new List<SelectListItem>();

        public async Task<IActionResult> OnGet()
        {
            if (HttpContext.Session.GetString("role") != "customer")
            {
                return RedirectToPage("/Index");
            }
            await UpdateBalance();
            await UpdateAccounts();
            return Page();
        }
        public async Task OnPostUpdate()
        {
            await UpdateBalance();
            await UpdateAccounts();
        }

        public async Task OnPostDeposit(int accountId, decimal amount)
        {
            await _transactionService.DepositAsync(accountId, amount);
            await UpdateBalance();
            await UpdateAccounts();
        }
        public async Task OnPostWithdraw(int accountId, decimal amount)
        {
            await _transactionService.WithdrawAsync(accountId, amount);
            await UpdateBalance();
            await UpdateAccounts();
        }
        private async Task UpdateAccounts()
        {
            var accounts = await _bankAccountService.GetBankAccountsByUserId((int)HttpContext.Session.GetInt32("Id"));
            BankAccounts = accounts.Select(acc => new SelectListItem
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
