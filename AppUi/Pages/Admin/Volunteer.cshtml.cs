using System;
using System.Threading.Tasks;
using AppDataAccess.Repositories;
using AppModels.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AppUi.Pages.Admin
{
    public class VolunteerModel : PageModel
    {
        private readonly IUsersServices _userData;
        private readonly UserManager<IdentityUser> _userManager;

        public VolunteerModel(IUsersServices usersServices, UserManager<IdentityUser> userManager)
        {
            _userData = usersServices;
            _userManager = userManager;
        }

        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }
        public VolunteerViewModel ViewModel { get; set; }

        public async Task<IActionResult> OnGet()
        {
            if (Id == Guid.Empty) 
            {
                TempData["Warning"] = "No Volunteer selected";
                return RedirectToPage("/Admin/Volunteers");
            }

            var user = await _userData.GetUserAsync(Id);
            ViewModel = new VolunteerViewModel()
            {
                User = user,
                Identity = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == user.UserId)
            };

            ViewData["Title"] = $"{user.Name} Details";
            return Page();
        }
    }
}