using System.Threading.Tasks;
using AppDataAccess.Repositories;
using AppModels.DTO;
using AppModels.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppUi.Pages.Accounts
{
    public class AddContactModel : PageModel
    {
        private readonly IUsersServices _userData;

        public AddContactModel(IUsersServices usersServices)
        {
            _userData = usersServices;
        }

        [BindProperty()]
        public AddContactViewModel ViewModel { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost() 
        {
            if (!ModelState.IsValid) 
            {
                TempData["Warning"] = "The values entered for a new contact are not correct, try again";
                return RedirectToPage("/Accounts/Profile");
            }

            Contact contact = new Contact() 
            {
                UserId = _userData.GetUserByUserIdAsync(ViewModel.UserId).Result.Id,
                Street = ViewModel.Street,
                Address1 = ViewModel.Address1,
                Address2 = ViewModel.Address2,
                Country = ViewModel.Country,
                Telephone = ViewModel.Telephone,
                Type = ViewModel.Type,
                Zipcode = ViewModel.Zipcode
            };

            _ = await _userData.AddContactAsync(contact);
            return RedirectToPage("/Accounts/Profile");
        }
    }
}