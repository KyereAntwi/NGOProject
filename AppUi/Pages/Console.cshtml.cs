using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppUi.Pages
{
    [Authorize]
    public class ConsoleModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;

        public ConsoleModel(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> OnGet()
        {
            var user = await _userManager.GetUserAsync(User);

            if (await _userManager.IsInRoleAsync(user, "Administrator"))
                return RedirectToPage("/Admin/Index");
            else if (await _userManager.IsInRoleAsync(user, "Volunteer"))
                return RedirectToPage("/Volunteer/Index");
            else
                return RedirectToPage("/Index");
        }
    }
}