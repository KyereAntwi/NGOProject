using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AppDataAccess.Repositories;
using AppModels.DTO;
using AppModels.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppUi.Pages.Volunteer
{
    public class WriteLetterModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUsersServices _userData;
        private readonly ILettersServices _letterServices;

        public WriteLetterModel(UserManager<IdentityUser> userManager, IUsersServices usersServices, ILettersServices lettersServices)
        {
            _userManager = userManager;
            _userData = usersServices;
            _letterServices = lettersServices;
        }

        public WriteLetterViewModel ViewModel { get; set; }
        public ApplicationUser user { get; set; }

        public async Task<IActionResult> OnGet()
        {
            ViewModel = new WriteLetterViewModel();
            ViewModel.Children = new List<Child>();

            user = await _userData.GetUserByUserIdAsync(_userManager.GetUserAsync(User).Result.Id);

            if (user.Children != null) 
            {
                ViewModel.Children = await _userData.GetSupportingChildren(user.UserId);
            }

            return Page();
        }

        public async Task<IActionResult> OnPost() 
        {
            if (!ModelState.IsValid) 
            {
                TempData["Warning"] = "The values provided are not correct, try again";
                return RedirectToPage();
            }

            Letter newLetter = new Letter 
            {
                ApplicationUserId = user.Id,
                DateWritten = DateTime.Now,
                Details = ViewModel.Details,
                ChildId = ViewModel.ChildId,
                Title = ViewModel.Title,
                Seen = false
            };

            _ = await _letterServices.AddLetterAsync(newLetter);

            TempData["Success"] = "Letter sent successfully";
            return RedirectToPage();
        }
    }
}