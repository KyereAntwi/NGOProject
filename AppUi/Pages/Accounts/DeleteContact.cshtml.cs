using System;
using System.Threading.Tasks;
using AppDataAccess.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppUi.Pages.Accounts
{
    [Authorize]
    public class DeleteContactModel : PageModel
    {
        private readonly IUsersServices _userData;

        public DeleteContactModel(IUsersServices usersServices)
        {
            _userData = usersServices;
        }

        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        public async Task<IActionResult> OnGet()
        {
            _ = await _userData.DeleteContactAsync(Id);
            return RedirectToPage("/Accounts/Profile");
        }
    }
}