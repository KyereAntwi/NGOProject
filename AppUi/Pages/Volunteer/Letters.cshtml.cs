using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppDataAccess.Repositories;
using AppModels.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppUi.Pages.Volunteer
{
    public class LettersModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userData;
        private readonly IUsersServices _user;

        public LettersModel(UserManager<IdentityUser> userManager,
                            IUsersServices usersServices)
        {
            _userData = userManager;
            _user = usersServices;
        }

        public List<Letter> Letters { get; set; }

        public async Task<IActionResult> OnGet()
        {
            var user = await _user.GetUserByUserIdAsync(_userData.GetUserAsync(User).Result.Id);
            Letters = new List<Letter>();
            Letters = user.Letters.ToList();
            return Page();
        }
    }
}