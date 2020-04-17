using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AppDataAccess.Repositories;
using AppModels.DTO;
using AppModels.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppUi.Pages.Admin
{
    [Authorize(Roles = "Administrator")]
    public class GalleriesModel : PageModel
    {
        private readonly IGalleryServices _data;

        public GalleriesModel(IGalleryServices galleryServices)
        {
            _data = galleryServices;
        }

        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }
        [BindProperty()]
        public GalleriesViewModel ViewModel { get; set; }

        public async Task<IActionResult> OnGet()
        {
            await GetData();
            return Page();
        }

        public async Task<IActionResult> OnPost() 
        {
            if (!ModelState.IsValid) 
            {
                TempData["Warning"] = "Make sure all entries are not empty";
                await GetData();
                return Page();
            }

            Gallery gallery = new Gallery 
            {
                DateAdded = DateTime.Now,
                Description = ViewModel.Description
            };

            // process file upload
            if (ViewModel.Image != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await ViewModel.Image.CopyToAsync(memoryStream);
                    gallery.Image = memoryStream.ToArray();
                }
            }

             _= await _data.AddGalleryAsync(gallery);

            await GetData();
            return Page();
        }

        async Task GetData() 
        {
            ViewModel = new GalleriesViewModel();
            ViewModel.Galleries = new List<Gallery>();

            ViewModel.Galleries = await _data.GalleriesAsync();
        }
    }
}