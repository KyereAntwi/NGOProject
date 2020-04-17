using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using AppDataAccess.Repositories;
using AppModels.DTO;
using AppModels.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppUi.Pages.Accounts
{
    [Authorize]
    public class ProfileModel : PageModel
    {
        private readonly IUsersServices _userData;
        private readonly UserManager<IdentityUser> _identityData;

        public ProfileModel(IUsersServices usersServices, UserManager<IdentityUser> userManager)
        {
            _userData = usersServices;
            _identityData = userManager;
        }

        public UserSummaryViewModel ViewModel { get; set; }
        [BindProperty]
        public InputPasswordModel ChangePassword { get; set;  }

        public async Task<IActionResult> OnGet()
        {
            var identityUser = await _identityData.GetUserAsync(User);
            var user = await _userData.GetUserByUserIdAsync(identityUser.Id);

            ViewData["Title"] = $"{user.Name} Accounts";

            ViewModel = new UserSummaryViewModel();
            ViewModel.User = new ApplicationUser();
            ViewModel.Identity = new IdentityUser();

            ViewModel.Identity = identityUser;
            ViewModel.User = user;

            return Page();
        }

        public async Task<IActionResult> OnPost() 
        {
            if (!ModelState.IsValid) 
            {
                TempData["Warning"] = "The values entered are not correctly entered, try again";
                return RedirectToPage();
            }

            var response = await _identityData.ChangePasswordAsync(await _identityData.GetUserAsync(User), ChangePassword.OldPassword, ChangePassword.NewPassword);
            if (response.Succeeded)
            {
                TempData["Success"] = "Password was successfully changed";
            }
            else 
            {
                string errorString = "Error:";
                foreach(var item in response.Errors) 
                {
                    errorString = errorString + ", " + item.Description;
                }
                TempData["Failed"] = $"{errorString}";
            }

            return RedirectToPage();
        }

        public class InputPasswordModel
        {
            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Old password")]
            public string OldPassword { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "New password")]
            public string NewPassword { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm new password")]
            [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }
    }
}