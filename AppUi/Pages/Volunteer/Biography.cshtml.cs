using System;
using System.IO;
using System.Threading.Tasks;
using AppDataAccess.Repositories;
using AppModels.DTO;
using AppModels.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppUi.Pages.Volunteer
{
    public class BiographyModel : PageModel
    {
        private readonly IUsersServices _services;

        public BiographyModel(IUsersServices services)
        {
            _services = services;
        }

        [BindProperty(SupportsGet = true)]
        public string UserId { get; set; }
        [BindProperty]
        public NewStaffViewModel ViewModel { get; set; }
        public string ReturnUrl { get; set; }

        public async Task<IActionResult> OnGet(string returnUrl = null)
        {
            var user = await _services.GetUserByUserIdAsync(UserId);
            if (user == null)
            {
                ViewData["Title"] = "Add Biography Data";
            }
            else
            {
                ViewData["Title"] = "Update Biography Data";
                ViewModel = new NewStaffViewModel();
                ViewModel.DateOfBirth = user.DateOfBirth.Date;
                ViewModel.Gender = user.Gender;
                ViewModel.Name = user.Name;
                ViewModel.Nationality = user.Nationality;
            }

            ReturnUrl = returnUrl;
            return Page();
        }

        public async Task<IActionResult> OnPost() 
        {
            if (!ModelState.IsValid || string.IsNullOrWhiteSpace(UserId))
                return Page();

            ApplicationUser user = new ApplicationUser 
            {
                Name = ViewModel.Name,
                Gender = ViewModel.Gender,
                DateOfBirth = ViewModel.DateOfBirth.Date,
                DateAdded = DateTime.Now,
                Nationality = ViewModel.Nationality,
                UserId = UserId
            };

            var response = await _services.AddUserAsync(user);

            if (response == null) 
            {
                TempData["Failed"] = "";
                RedirectToPage("/Areas/Identity/Account/Register");
            }

            TempData["Success"] = "Registration was successful";
            return RedirectToPage("/Volunteer/Index");
        }
    }
}
