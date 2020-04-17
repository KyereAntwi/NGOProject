using System;
using System.Threading.Tasks;
using AppDataAccess.Repositories;
using AppModels.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppUi.Pages
{
    public class UserImageModel : PageModel
    {
        private readonly IUsersServices _services;

        public UserImageModel(IUsersServices services)
        {
            _services = services;
        }

        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        public async Task<FileContentResult> OnGet()
        {
            if (Id == Guid.Empty)
                return null;

            ApplicationUser user = await _services.GetUserAsync(Id);

            if (user == null)
                return null;

            return File(user.Photograph, "image/jpeg");
        }
    }
}