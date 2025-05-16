using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Simple_Bank_Application.Services.Interfaces;

namespace Simple_Bank_Application.Pages.admin
{
    public class delete_userModel : PageModel
    {
        private readonly IUserService _service;

        public delete_userModel(IUserService service) => _service = service;

        [BindProperty]
        public int Id { get; set; }
        public bool Deleted { get; set; } = true;
        public string Message { get; set; }

        public async Task OnPost()
        {
            Deleted = await _service.DeleteUserAsync(Id);

            if (Deleted)
                Message = "Successfully Deleted";
            else
                Message = "User not found";
        }
    }
}
