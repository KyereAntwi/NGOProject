using AppModels.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AppUi.ViewComponents
{
    public class AddContact : ViewComponent
    {
        private readonly UserManager<IdentityUser> _userManager;

        public AddContact(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync() 
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            AddContactViewModel ViewModel = new AddContactViewModel() 
            {
                UserId = user.Id
            };
            return View(ViewModel);
        }
    }
}
