using System.Collections.Generic;
using System.Threading.Tasks;
using AppDataAccess.Repositories;
using AppModels.DTO;
using AppModels.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppUi.Pages
{
    public class GalleriesModel : PageModel
    {
        private readonly IGalleryServices _data;

        public GalleriesModel(IGalleryServices galleryServices)
        {
            _data = galleryServices;
        }

        public GalleriesViewModel ViewModel { set; get; }

        public async Task<IActionResult> OnGet()
        {
            ViewModel = new GalleriesViewModel();
            ViewModel.Galleries = new List<Gallery>();

            ViewModel.Galleries = await _data.GalleriesAsync();
            return Page();
        }
    }
}