using System;
using System.Threading.Tasks;
using AppDataAccess.Repositories;
using AppModels.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppUi.Pages
{
    public class ChildImageModel : PageModel
    {
        private readonly IChildServices _services;

        public ChildImageModel(IChildServices childServices)
        {
            _services = childServices;
        }

        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        public async Task<FileContentResult> OnGet()
        {
            if (Id == Guid.Empty)
                return null;

            Child user = await _services.GetAChildAsync(Id);

            if (user == null)
                return null;

            return File(user.Phtotograph, "image/jpeg");
        }
    }
}