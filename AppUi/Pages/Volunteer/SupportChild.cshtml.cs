using System.Threading.Tasks;
using AppDataAccess.Repositories;
using AppModels.DTO;
using AppModels.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppUi.Pages.Volunteer
{
    public class SupportChildModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userData;
        private readonly IUsersServices _appUserData;
        private readonly IChildServices _data;

        public SupportChildModel(UserManager<IdentityUser> userManager, 
                                 IUsersServices userServices,
                                 IChildServices childServices)
        {
            _userData = userManager;
            _appUserData = userServices;
            _data = childServices;
        }

        [BindProperty()]
        public SupportChildViewModel ViewModel { get; set; }

        public void OnGet() { }

        public async Task<IActionResult> OnPost()
        {
            var user = await _userData.GetUserAsync(User);

            var volunteerNow = new ChildSponser
            {
                ApplicationUserId = _appUserData.GetUserByUserIdAsync(user.Id).Result.Id,
                ChildId = ViewModel.Child.Id,
                Accepted = false
            };

            _ = await _data.SponcerAChildAsync(volunteerNow);

            TempData["Success"] = $"Request to support {ViewModel.Child.Fullname.ToUpper()} has been successfully submitted. You would be contacted and then your request approved. Thank you for your concern";
            return RedirectToPage("/Volunteer/Index");
        }
    }
}