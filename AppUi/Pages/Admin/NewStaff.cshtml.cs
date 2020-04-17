using System;
using System.IO;
using System.Threading.Tasks;
using AppDataAccess.Repositories;
using AppModels.DTO;
using AppModels.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppUi.Pages.Admin
{
    [Authorize(Roles = "Administrator")]
    public class NewStaffModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUsersServices _userServices;

        public NewStaffModel(UserManager<IdentityUser> userManager, IUsersServices usersServices)
        {
            _userManager = userManager;
            _userServices = usersServices;
        }

        [BindProperty]
        public NewStaffViewModel ViewModel { get; set; }

        public IActionResult OnGet()
        {
            ViewData["Title"] = "New Staff";
            return Page();
        }

        public async Task<IActionResult> OnPost() 
        {
            if (!ModelState.IsValid)
                return Page();
            
            ApplicationUser applicationUser = new ApplicationUser
            {
                Name = ViewModel.Name,
                Gender = ViewModel.Gender,
                DateAdded = DateTime.Now,
                DateOfBirth = ViewModel.DateOfBirth.Date,
                Nationality = ViewModel.Nationality
            };

            // process file upload
            if (ViewModel.Photograph != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await ViewModel.Photograph.CopyToAsync(memoryStream);
                    // Upload the file if less than 2 MB        
                    if (memoryStream.Length < 2097152)
                    {
                        applicationUser.Photograph = memoryStream.ToArray();
                    }
                    else { ModelState.AddModelError("File", "The file is too large."); }
                }
            }

            IdentityUser identityUser = new IdentityUser 
            {
                Email = ViewModel.Email,
                UserName = ViewModel.Email,
                PhoneNumber = ViewModel.Phone
            };

            var user = await _userManager.CreateAsync(identityUser, $"StaffPassword@{ViewModel.DateOfBirth.Year}");

            if (user.Succeeded) 
            {
                var newUser = await _userManager.FindByNameAsync(ViewModel.Email);
                var roleResponse = await _userManager.AddToRoleAsync(newUser, "Administrator");

                if (roleResponse.Succeeded) 
                {
                    applicationUser.UserId = newUser.Id;
                    var appUser = await _userServices.AddUserAsync(applicationUser);

                    if (appUser != null) 
                    {

                        TempData["Success"] = "Staff was created successfully";
                        return RedirectToPage("/Admin/Staffs");
                    }
                    else
                    {
                        TempData["Failed"] = "The operation failed, try again";
                        _= await _userManager.DeleteAsync(newUser);
                    }
                }
            }

            return Page();
        }
    }
}