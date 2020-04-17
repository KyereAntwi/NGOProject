using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDataAccess.Repositories;
using AppModels.DTO;
using AppModels.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppUi.Pages.Admin
{
    public class StaffModel : PageModel
    {
        private readonly IUsersServices _user;
        private readonly UserManager<IdentityUser> _userManager;

        public StaffModel(IUsersServices usersServices, UserManager<IdentityUser> userManager)
        {
            _user = usersServices;
            _userManager = userManager;
        }

        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }
        public StaffDetailsViewModel ViewModel { get; set; }

        public async Task<IActionResult> OnGet()
        {
            if (Id == Guid.Empty)
                return RedirectToPage("/Admin/Staffs");

            ViewModel = new StaffDetailsViewModel();
            ViewModel.User = new IdentityUser();
            ViewModel.AppUser = new ApplicationUser();
            ViewModel.TimeLines = new List<TimeLine>();

            ViewModel.User = await _userManager.GetUserAsync(User);
            ViewModel.AppUser = await _user.GetUserAsync(Id);
            ViewModel.TimeLines = ViewModel.AppUser.TimeLines.ToList();

            return Page();
        }
    }
}