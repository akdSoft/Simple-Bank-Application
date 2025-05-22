using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Simple_Bank_Application.Models;
using Simple_Bank_Application.Services.Interfaces;

namespace Simple_Bank_Application.Pages.admin
{
    public class list_all_transactionsModel : PageModel
    {
        private readonly ITransactionService _service;

        public list_all_transactionsModel(ITransactionService service) => _service = service;

        public List<Transaction> Transactions { get; set; }

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("role") != "admin")
            {
                return RedirectToPage("/Index");
            }
            return Page();
        }
        public async Task OnPost()
        {
            Transactions = (List<Transaction>) await _service.GetAllTransactionsAsync();
        }
    }
}
