using System.Threading.Tasks;
using AppDataAccess.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppUi.Pages.Accounts
{
    [Authorize]
    public class DeleteModel : PageModel
    {
        private readonly UserManager<IdentityUser> _identity;
        private readonly IUsersServices _user;
        private readonly SignInManager<IdentityUser> _signIn;

        public DeleteModel(UserManager<IdentityUser> userManager, IUsersServices usersServices, SignInManager<IdentityUser> signInManager)
        {
            _identity = userManager;
            _user = usersServices;
            _signIn = signInManager;
        }

        public async Task<IActionResult> OnGet()
        {
            if (_signIn.IsSignedIn(User)) 
            {
                var user = await _identity.GetUserAsync(User);
                var response = await _identity.DeleteAsync(user);

                if (response.Succeeded) 
                {
                    var removedUser = await _user.GetUserByUserIdAsync(user.Id);
                    _= await _user.DeleteUserAsync(removedUser.Id);

                    await _signIn.SignOutAsync();
                    return RedirectToPage("/Index");
                }

                TempData["Failed"] = "Sorry something went wrong, try again";
                return RedirectToPage();
            }

            return RedirectToPage("/Index");
        }
    }
}