using System.Collections.Generic;
using System.Threading.Tasks;
using AppDataAccess.Repositories;
using AppModels.DTO;
using AppModels.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppUi.Pages.Admin
{
    public class StaffsModel : PageModel
    {
        private readonly IVolunteerServices _volunteerServices;
        private readonly UserManager<IdentityUser> _userManager;

        public StaffsModel(IVolunteerServices volunteerServices, UserManager<IdentityUser> userManager)
        {
            _volunteerServices = volunteerServices;
            _userManager = userManager;
        }

        public VolunteersViewModel ViewModel { get; set; }

        public async Task<IActionResult> OnGet()
        {
            ViewData["Title"] = "Staff List";
            ViewModel = new VolunteersViewModel();
            ViewModel.Volunteers = new List<ApplicationUser>();

            var list = await _volunteerServices.GetApplicationUsersAsync();

            foreach (var volunteer in list) 
            {
                var user = await _userManager.FindByIdAsync(volunteer.UserId);
                if (await _userManager.IsInRoleAsync(user, "Administrator"))
                {
                    ViewModel.Volunteers.Add(volunteer);
                }
            }

            return Page();
        }
    }
}