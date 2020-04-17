using System;
using System.Threading.Tasks;
using AppDataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppUi.Pages
{
    public class GalleryImageModel : PageModel
    {
        private readonly IGalleryServices _data;

        public GalleryImageModel(IGalleryServices galleryServices)
        {
            _data = galleryServices;
        }

        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        public async Task<FileContentResult> OnGet()
        {
            if (Id == Guid.Empty)
                return null;

            var gallery = await _data.GalleryAsync(Id);

            if (gallery == null)
                return null;

            return File(gallery.Image, "image/jpeg");
        }
    }
}