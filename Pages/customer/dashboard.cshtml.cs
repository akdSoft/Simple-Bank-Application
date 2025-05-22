using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Simple_Bank_Application.Services.Interfaces;

namespace Simple_Bank_Application.Pages.customer
{
    public class dashboardModel : PageModel
    {
        private readonly IBankAccountService _service;
        public dashboardModel(IBankAccountService service) => _service = service;

        [BindProperty]
        public int? SelectedId { get; set; }
        public decimal TotalBalance { get; set; } = 0.00m;
        public decimal CurrentBalance { get; set; } = 0.00m;
        public List<SelectListItem> BankAccounts { get; set; } = new List<SelectListItem>();

        public async Task<IActionResult> OnGet()
        {
            if (HttpContext.Session.GetString("role") != "customer")
            {
                return RedirectToPage("/Index");
            }
            await UpdateAll();
            return Page();
        }

        public async Task OnPostUpdate()
        {
            await UpdateAll();
        }

        public async Task OnPostDeleteAccount(int id)
        {
            await _service.DeleteBankAccountAsync(id);
            await UpdateAll();

        }
        public IActionResult OnPostLogout()
        {
            HttpContext.Session.Clear();
            return RedirectToPage("/Index");
        }

        private async Task UpdateAll()
        {
            await UpdateAccounts();
            await UpdateCurrentBalance();
            await UpdateTotalBalance();
        }
        private async Task UpdateAccounts()
        {
            var accounts = await _service.GetBankAccountsByUserId((int)HttpContext.Session.GetInt32("Id"));

            BankAccounts = accounts.Select(acc => new SelectListItem
            {
                Value = acc.Id.ToString(),
                Text = $"{acc.Id}"
            }).ToList();
        }
        private async Task UpdateTotalBalance()
        {
            decimal balanceCounter = 0.00m;
            var accounts = await _service.GetBankAccountsByUserId((int)HttpContext.Session.GetInt32("Id"));
            foreach (var acc in accounts)
            {
                balanceCounter += acc.Balance;
            }
            TotalBalance = balanceCounter;
        }
        private async Task UpdateCurrentBalance()
        {
            if (SelectedId != null)
            {
                var acc = await _service.GetBankAccountByIdAsync(SelectedId.Value);
                if (acc != null)
                    CurrentBalance = acc.Balance;
            }
        }
    }
}
