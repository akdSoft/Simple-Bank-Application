using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Simple_Bank_Application.Models;
using Simple_Bank_Application.Services.Interfaces;

namespace Simple_Bank_Application.Pages.customer
{
    public class dashboardModel : PageModel
    {
        private readonly IBankAccountService _service;
            
        public dashboardModel(IBankAccountService service) => _service = service;

        [BindProperty]
        public string SelectedId { get; set; }
        public decimal TotalBalance { get; set; }
        public decimal CurrentBalance { get; set; }
        public List<SelectListItem> BankAccounts { get; set; } = new List<SelectListItem>();
        public async Task OnGet()
        {
            await UpdateAccounts();
            await UpdateCurrentBalance();
            await UpdateTotalBalance();
        }

        public async Task OnPost()
        {
            await UpdateAccounts();
            await UpdateCurrentBalance();
            await UpdateTotalBalance();
            Console.WriteLine("selected id:" + SelectedId);
        }

        public async Task UpdateCurrentBalance()
        {
            if(SelectedId != null)
            {
                var acc = await _service.GetBankAccountByIdAsync(Int32.Parse(SelectedId));
                if(acc != null)
                    CurrentBalance = acc.Balance;
            }
        }
        public async Task UpdateTotalBalance()
        {
            decimal balanceCounter = 0.00m;
            var accounts = await _service.GetBankAccountsByUserId((int)HttpContext.Session.GetInt32("Id"));
            foreach(var acc in accounts)
            {
                balanceCounter += acc.Balance;
            }
            TotalBalance = balanceCounter;
        }
        public async Task UpdateAccounts()
        {
            var accounts = await _service.GetBankAccountsByUserId((int)HttpContext.Session.GetInt32("Id"));

            BankAccounts = accounts.Select(acc => new SelectListItem
            {
                Value = acc.Id.ToString(),
                Text = $"{acc.Id}"
            }).ToList();
        }
        public async Task<IActionResult> OnPostDeleteAccount()
        {
            Console.WriteLine("testing");
            if(SelectedId != null)
            {
                await _service.DeleteBankAccountAsync(Int32.Parse(SelectedId));
                await UpdateAccounts();
                await UpdateCurrentBalance();
                await UpdateTotalBalance();
                
            }
            return Page();
        }
        public IActionResult OnPostLogout()
        {
            HttpContext.Session.Clear();
            return RedirectToPage("/Index");
        }
    
    }
}
