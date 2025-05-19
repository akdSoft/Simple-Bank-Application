using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Simple_Bank_Application.Models;
using Simple_Bank_Application.Services.Interfaces;

namespace Simple_Bank_Application.Pages.customer;

public class transaction_historyModel : PageModel
{
    private readonly ITransactionService _service;

    public transaction_historyModel(ITransactionService service) => _service = service;

    public List<Transaction> Transactions { get; set; }
    public async Task OnGet()
    {
        
    }

    public async Task OnPost()
    {
        var transactions = await _service.GetTransactionsByUserAsync((int)HttpContext.Session.GetInt32("Id"));
        Transactions = transactions.ToList();
    }
}
