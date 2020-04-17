using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDataAccess.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppUi.Pages.Volunteer
{
    [Authorize(Roles = "Volunteer")]
    public class IndexModel : PageModel
    {
        private readonly UserManager<IdentityUser> _user;
        private readonly IUsersServices _data;

        public IndexModel(UserManager<IdentityUser> userManager, IUsersServices usersServices)
        {
            _user = userManager;
            _data = usersServices;
        }

        public async Task<IActionResult> OnGet()
        {
            var user = await _user.GetUserAsync(User);

            var personalData = await _data.GetUserByUserIdAsync(user.Id);

            if (personalData == null)
                return RedirectToPage("/Volunteer/Biography", new { UserId = user.Id });

            ViewData["Title"] = $"{personalData.Name} Dashboard";
            return Page();
        }
    }
}
