using System.IO;
using System.Threading.Tasks;
using AppDataAccess.Repositories;
using AppModels.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppUi.Pages.Accounts
{
    public class UpdatePictureModel : PageModel
    {
        private readonly IUsersServices _userServices;

        public UpdatePictureModel(IUsersServices usersServices)
        {
            _userServices = usersServices;
        }

        [BindProperty()]
        public UpdatePictureViewModel ViewModel { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost() 
        {
            if (!ModelState.IsValid)
                return RedirectToPage("");

            // process file upload
            if (ViewModel.Photograph != null)
            {
                var user = await _userServices.GetUserAsync(ViewModel.Id);
                using (var memoryStream = new MemoryStream())
                {
                    await ViewModel.Photograph.CopyToAsync(memoryStream);
                    // Upload the file if less than 2 MB        
                    if (memoryStream.Length < 2097152)
                    {
                        user.Photograph = memoryStream.ToArray();
                    }
                    else
                    {
                        TempData["Warning"] = "Sorry! the profile picture provided should not be more than 2mb";
                        return RedirectToPage("/Accounts/Profile");
                    }
                }

                _= await _userServices.UpdateUserAsync(user);
                return RedirectToPage("/Accounts/Profile");
            }
            else 
            {
                TempData["Warning"] = "A file was not submitted";
                return RedirectToPage("/Accounts/Profile");
            }
        }
    }
}
